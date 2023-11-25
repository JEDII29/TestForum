using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using TestForum.API.Abstract;
using TestForum.API.Services;
using TestForum.Data;
using TestForum.Data.Entities;
using AutoMapper;
using TestForum.API.Infrastructure;
using TestForum.Data.Seeds;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var rsaKey = RSA.Create();
rsaKey.ImportRSAPrivateKey(File.ReadAllBytes("key"), out _);
var key = new RsaSecurityKey(rsaKey);
//var privateKey = rsaKey.ExportRSAPrivateKey();
//var jwtSettings = builder.Configuration.GetSection("JwtSettings");
//var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value));

builder.Services.AddDbContext<ForumDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("LocalConnection")));

builder.Services.AddIdentity<UserEntity, IdentityRole<Guid>>(o=>
	{
		o.Password.RequireNonAlphanumeric = false;
		o.Password.RequireDigit = false;
		o.Password.RequiredLength = 4;
	})
	.AddEntityFrameworkStores<ForumDbContext>()
	.AddDefaultTokenProviders();
#region
//builder.Services.AddAuthentication("jwt")
//	.AddJwtBearer("jwt", o =>
//	{
//		o.Events = new JwtBearerEvents()
//		{
//			OnMessageReceived = (ctx) =>
//			{
//				if (ctx.Request.Query.ContainsKey("t"))
//				{
//					ctx.Token = ctx.Request.Query["t"];
//				}
//				return Task.CompletedTask;
//			}
//		};
//	});
#endregion


builder.Services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	{
		options.RequireHttpsMetadata = false;
		options.SaveToken = true;
		options.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuer = false,
			ValidateAudience = false,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = key
			
		};
	});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("adminRole",
		 policy => policy.RequireRole("admin"));
	options.AddPolicy("userRole",
	 policy => policy.RequireRole("user"));
});


builder.Services.AddControllers();

builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();
builder.Services.AddScoped<IUsersService, DbUserService>();
builder.Services.AddScoped<IReputationService, ReputationService>();
builder.Services.AddScoped<IVotesService, VotesService>();
builder.Services.AddScoped<TestForum.API.Abstract.IAuthenticationService, TestForum.API.Services.AuthenticationService>();
builder.Services.AddScoped<UserManager<UserEntity>>();
builder.Services.AddScoped<MapperProfile>();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestForum", Version = "v1" });

	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Description = "JWT Authorization header using the Bearer scheme",
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT"
	});

	c.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			new string[] {}
		}
	});

});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var rolesToAdd = new[] { "admin", "moder", "user" };
	var RoleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
	var UsrMgr = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
	var DbCntx = new ForumDbContext(scope.ServiceProvider.GetRequiredService<DbContextOptions<ForumDbContext>>());
	var PswdHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<UserEntity>>();

	foreach (var roleName in rolesToAdd)
	{
		// Sprawdź, czy rola już istnieje w bazie danych
		if (!RoleMgr.RoleExistsAsync(roleName).Result)
		{
			// Jeśli nie istnieje, dodaj nową rolę

			var role = new IdentityRole<Guid>(roleName);
			var result = RoleMgr.CreateAsync(role).Result;

			if (result.Succeeded)
			{
				Console.WriteLine($"Rola {roleName} została dodana.");
			}
			else
			{
				Console.WriteLine($"Błąd podczas dodawania roli {roleName}.");
			}
		}
	}
	if (!DbCntx.Users.Any())
	{
		var account1 = new UserEntity("Administrator")
		{
			Reputation = 1000
		};

		var account2 = new UserEntity("Moderator1")
		{
			Reputation = 500
		};

		var account3 = new UserEntity("Moderator2")
		{
			Reputation = 500
		};

		var account4 = new UserEntity("User1")
		{
			Reputation = 100
		};

		var account5 = new UserEntity("User1")
		{
			Reputation = 100
		};

		await UsrMgr.CreateAsync(account1, password: "Password");
		await UsrMgr.AddToRoleAsync(account1, "admin");
		await UsrMgr.CreateAsync(account2, password: "Password");
		await UsrMgr.AddToRoleAsync(account2, "moder");
		await UsrMgr.CreateAsync(account3, password: "Password");
		await UsrMgr.AddToRoleAsync(account3, "moder");
		await UsrMgr.CreateAsync(account4, password: "Password");
		await UsrMgr.AddToRoleAsync(account4, "user");
		await UsrMgr.CreateAsync(account5, password: "Password");
		await UsrMgr.AddToRoleAsync(account5, "user");

	}
}


app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestForum");
		c.RoutePrefix = "swagger";
	});
}

app.MapControllers();

app.MapGet("/", () => "Hello World!");
app.Run();

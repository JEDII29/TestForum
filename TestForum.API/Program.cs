using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using TestForum.API.Abstract;
using TestForum.API.Services;
using TestForum.Data;
using TestForum.Data.Entities;
using AutoMapper;
using TestForum.API.Mappers;
using TestForum.Data.Seeds;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ForumDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("LocalConnection")));

builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
builder.Services.AddIdentity<UserEntity, IdentityRole<Guid>>(o=>
	{
		o.Password.RequireNonAlphanumeric = false;
	})
	.AddEntityFrameworkStores<ForumDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddAuthentication();

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUsersService, ExampleUsersService>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();

builder.Services.AddScoped<ArticleMapper>();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestForum", Version = "v1" });
});


var app = builder.Build();

// Dodaj role przy uruchamianiu aplikacji
using (var scope = app.Services.CreateScope())
{
	var rolesToAdd = new[] { "admin", "user" };
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
		var user1 = new UserEntity("Administrator")
		{
			Reputation = 100
		};

		var user2 = new UserEntity("user1")
		{
			Reputation = 150
		};

		var user3 = new UserEntity("user2")
		{
			Reputation = 75
		};
		await UsrMgr.CreateAsync(user1, password: "Password1");
		await UsrMgr.AddToRoleAsync(user1, "admin");
		await UsrMgr.CreateAsync(user2, password: "Password2");
		await UsrMgr.AddToRoleAsync(user2, "user");
		await UsrMgr.CreateAsync(user3, password: "Password3");
		await UsrMgr.AddToRoleAsync(user3, "user");
	}
}




if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestForum");
		c.RoutePrefix = "swagger";
	});
}

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();

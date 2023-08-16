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
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ForumDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("LocalConnection")));

builder.Services.AddScoped<IPasswordHasher<UserEntity>, PasswordHasher<UserEntity>>();
builder.Services.AddIdentity<UserEntity, IdentityRole<Guid>>()
	.AddEntityFrameworkStores<ForumDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddScoped<IUsersService, ExampleUsersService>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestForum", Version = "v1" });
});

builder.Services.AddScoped<ArticleMapper>();

var app = builder.Build();

var rolesToAdd = new[] { "admin", "user" };
// Dodaj role przy uruchamianiu aplikacji
using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
	UserSeed.SeedUsers(scope.ServiceProvider);
	ArticleSeed.SeedArticles(scope.ServiceProvider);
	foreach (var roleName in rolesToAdd)
	{
		// Sprawdź, czy rola już istnieje w bazie danych
		if (!roleManager.RoleExistsAsync(roleName).Result)
		{
			// Jeśli nie istnieje, dodaj nową rolę
			var role = new IdentityRole<Guid>(roleName);
			var result = roleManager.CreateAsync(role).Result;

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

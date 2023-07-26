using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using TestForum.API.Abstract;
using TestForum.API.Services;
using TestForum.Data;
using TestForum.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ForumDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("LocalConnection")));

builder.Services.AddIdentity<ApplicationUserEntity, IdentityRole<Guid>>()
	.AddEntityFrameworkStores<ForumDbContext>()
	.AddDefaultTokenProviders();

builder.Services.AddScoped<IUsersService, ExampleUsersService>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();

var app = builder.Build();

var rolesToAdd = new[] { "admin", "user" };
// Dodaj role przy uruchamianiu aplikacji
using (var scope = app.Services.CreateScope())
{
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

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
}

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();

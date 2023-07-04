using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using TestForum.API.Abstract;
using TestForum.API.Services;
using TestForum.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection"),
		x => x.MigrationsAssembly("TestForum.DatabaseContext")));

builder.Services.AddScoped<IUsersService, ExampleUsersService>();
builder.Serwices.AddScoped<IArticlesService, ArticlesService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

 app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();

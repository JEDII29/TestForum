using Microsoft.OpenApi.Models;
using TestForum.API.Abstract;
using TestForum.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsersService, ExampleUsersService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

 app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();

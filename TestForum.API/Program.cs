using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using TestForum.API.Abstract;
using TestForum.API.Services;
using TestForum.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseSqlServer(
		builder.Configuration.GetConnectionString("DefaultConnection"),
		x => x.MigrationsAssembly("TestForum.DatabaseContext")));

builder.Services.AddScoped<IUsersService, ExampleUsersService>();
builder.Services.AddScoped<IArticlesService, ArticlesService>();

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestForum", Version = "v1" });
});

var app = builder.Build();

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

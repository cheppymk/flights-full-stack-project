using Microsoft.OpenApi.Models;
using Flights.Server.Data;
// Other necessary imports...

var builder = WebApplication.CreateBuilder(args);

// Configure the database context
Entities.ConfigureDbContext(builder.Services, builder.Configuration);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.DescribeAllParametersInCamelCase();
    c.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "https://localhost:7280"
    });
    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"]}{e.ActionDescriptor.RouteValues["controller"]}");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseCors(builder => builder
    .WithOrigins("*")
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseSwagger().UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();

using Microsoft.EntityFrameworkCore;
using DotNetMicroDemo.Models;
using DotNetMicroDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = ".NET Micro Demo API", Version = "v1" });
});

// Configure Entity Framework with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=demo.db"));

// Register services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<CVDataService>();
builder.Services.AddHttpClient<LLMService>();
builder.Services.AddScoped<SmartChatbotService>();
builder.Services.AddScoped<ContentModerationService>();

// Add CORS support
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add logging
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
    logging.AddDebug();
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET Micro Demo API v1");
    c.RoutePrefix = "api-docs"; // Swagger UI at /api-docs
});

// Serve static files (must come before routing)
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync();
}

// Demo endpoint for quick testing
app.MapGet("/health", () => new
{
    Status = "Healthy",
    Timestamp = DateTime.UtcNow,
    Features = new[] { "Async/Await", "Retry Policy", "SQLite Database", "REST API" }
});

// Serve the portfolio page at root
app.MapGet("/", async () =>
{
    var htmlPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "portfolio.html");
    if (File.Exists(htmlPath))
    {
        var html = await File.ReadAllTextAsync(htmlPath);
        return Results.Content(html, "text/html");
    }
    return Results.Content("<h1>Portfolio page not found</h1><p>Check wwwroot/portfolio.html</p>", "text/html");
});

// Also serve the timer demo page
app.MapGet("/timer", async () =>
{
    var htmlPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "demo-timer.html");
    if (File.Exists(htmlPath))
    {
        var html = await File.ReadAllTextAsync(htmlPath);
        return Results.Content(html, "text/html");
    }
    return Results.Content("<h1>Timer demo not found</h1>", "text/html");
});
app.MapGet("/original", async () =>
{
    var htmlPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "index.html");
    if (File.Exists(htmlPath))
    {
        var html = await File.ReadAllTextAsync(htmlPath);
        return Results.Content(html, "text/html");
    }
    return Results.Content("<h1>Demo page not found</h1>", "text/html");
});

Console.WriteLine("🚀 .NET Micro Demo API is running!");
Console.WriteLine("📝 Demo Page: http://localhost:5000");
Console.WriteLine("📖 Swagger UI: http://localhost:5000/api-docs");
Console.WriteLine("🔍 Health Check: http://localhost:5000/health");
Console.WriteLine("📦 Products API: http://localhost:5000/api/products");

app.Run();
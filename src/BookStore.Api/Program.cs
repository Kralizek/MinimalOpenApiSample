using BookStore.Data;

using Microsoft.EntityFrameworkCore;

using MinimalOpenAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMinimalOpenApi();

var dataDirectory = Path.GetFullPath(Path.Combine(builder.Environment.ContentRootPath, "..", "..", ".data"));
Directory.CreateDirectory(dataDirectory);

var databasePath = Path.Combine(dataDirectory, "bookstore.db");
builder.Services.AddDbContext<BookstoreDbContext>(options => options.UseSqlite($"Data Source={databasePath}"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<BookstoreDbContext>();
	dbContext.Database.EnsureCreated();
}

app.MapGet("/", () => Results.Redirect("/swagger/index.html", permanent: false));

app.MapMinimalOpenApiEndpoints();

var schemas = app.MapOpenApiSchemas();

app.UseSwaggerUI(options =>
{
    foreach (var schema in schemas.Schemas)
    {
        options.SwaggerEndpoint(schema.PublicPath, schema.Name);
    }
});

app.Run();

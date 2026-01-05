var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// Route parameter
app.MapGet("/users/{userId}/posts/{slug}", (int userId, string slug) =>
{
    return $"User Id: {userId}, Post ID: {slug}";
});

// Route constraint: must be a positive int
app.MapGet("/products/{id:int:min(0)}", (int id) =>
{
  return $"Product: ID {id}";  
});

// Route with optional parameter
app.MapGet("/report/{year?}", (int? year = 2016) => {
    return $"Report for year: {year}";
});

// Catch all route
// Can be used if you need more complex filepaths
app.MapGet("/files/{*filePath}", (string filePath) =>
{
    return filePath;
});

// Query parameters
// q = query string
app.MapGet("/search", (string? q, int page = 1) =>
{
    return $"searching for {q} on page {page}";
});

// Combine, so multiple paths are possible and valid
app.MapGet("/store/{category}/{productId:int?}/{*extraPath}", (string category, int? productId, string? extraPath, bool inStock = true) =>
{});

app.Run();

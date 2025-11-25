var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Use with: http://localhost:5240/
app.MapGet("/", () => "Root Path");
// Use with: http://localhost:5240/downloads
app.MapGet("/downloads", () => "Downloads");

// The following are not used as intented
// This is only for practicing purposes
app.MapPut("/", () => "This is a put");
app.MapDelete("/", () => "Delete!!");
app.MapPost("/", () => "POST me");

app.Run();

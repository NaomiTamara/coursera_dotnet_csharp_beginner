var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Build requested features
var app = builder.Build();

// Exception handler
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("Home/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}

// Use the features we build earlier
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpLogging();

app.MapGet("/", () => "Hello, ASP.NET Core Middleware!");

// Custom middleware: you can recognize by 'async (context, next)'
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request Path: {context.Request.Path}");
    await next();
    Console.WriteLine($"Response Status Code: {context.Response.StatusCode}");
});

app.Use(async (context, next) =>
{
    var startTime = DateTime.UtcNow;
    Console.WriteLine($"Start Time: {DateTime.UtcNow}");
    await next();
    var duration = DateTime.UtcNow - startTime;
    Console.WriteLine($"Response Time: {duration.TotalMilliseconds} ms");
});

app.Run();

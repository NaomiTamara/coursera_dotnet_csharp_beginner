var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpLogging((o)=>{});

var app = builder.Build();

// Install Middleware
app.UseHttpLogging();
// Create Middleware
app.Use(async (context, next) =>
{
    // Add logic here: will run before the function next is called
    Console.WriteLine("Logic before");
    await next.Invoke();
    // Add logic here: will run after the function next is called
    Console.WriteLine("Logic after");
});

app.MapGet("/", () => "Hello World!");
app.MapGet("/hello", () => "This is the hello route!");

app.Run();

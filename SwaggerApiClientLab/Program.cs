// Program.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyApiClientNamespace;

public class Program
{
    public static async Task Main(string[] args)
    {
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		var app = builder.Build();

		app.UseSwagger();
		app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
		
		app.MapControllers();
		Task.Run(() => app.RunAsync());
		var httpClient = new HttpClient();
		var client = new GeneratedApiClient("http://localhost:5000", httpClient);
		
		var user = await client.GetUserAsync(1);

		app.Run();
    }
}
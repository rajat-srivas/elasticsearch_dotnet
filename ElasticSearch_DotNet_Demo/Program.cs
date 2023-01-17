using ElasticSearch_DotNet_Demo.ElasticSearch;
using ElasticSearch_DotNet_Demo.Repository;
using ElasticSearch_DotNet_Demo.Util;

namespace ElasticSearch_DotNet_Demo
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddScoped<ISearchIndexer, SearchIndexer>();
			builder.Services.AddScoped<ElasticClient>();
			builder.Services.AddScoped<TwitterClient>();
			
			

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
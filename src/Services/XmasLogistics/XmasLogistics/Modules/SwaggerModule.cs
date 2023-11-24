using Microsoft.OpenApi.Models;

namespace XmasLogistics.Modules
{
	public sealed class SwaggerModule : IModule
	{
		public bool IsEnabled => true;
		public int Order => 0;

		public IServiceCollection RegisterModule(WebApplicationBuilder builder)
		{
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
			{
				Description = "XmasDev Logistics",
				Title = "XmasDev Logistics",
				Version = "v1",
				Contact = new OpenApiContact
				{
					Name = "XmasLogistics"
				}
			}));

			return builder.Services;
		}

		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
	}
}
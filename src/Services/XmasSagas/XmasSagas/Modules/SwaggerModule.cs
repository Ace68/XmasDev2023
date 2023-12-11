using Microsoft.OpenApi.Models;

namespace XmasSagas.Modules
{
	public sealed class SwaggerModule : IModule
	{
		public bool IsEnabled => true;
		public int Order => 0;

		public IServiceCollection RegisterModule(WebApplicationBuilder builder)
		{
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo
			{
				Description = "Xmas Sagas",
				Title = "XmasSagas",
				Version = "v1",
				Contact = new OpenApiContact
				{
					Name = "Santa Claus",
					Email = "santa.claus@xmas.com",
					Url = new Uri("https://www.merrychristmas.com")
				}
			}));

			return builder.Services;
		}

		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
	}
}
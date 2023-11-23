using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace XmasWarehouses.Facade.Endpoints;

public static class WarehousesEndpoints
{
	public static IEndpointRouteBuilder MapWarehousesEndpoints(this IEndpointRouteBuilder endpoints)
	{
		var group = endpoints.MapGroup("/v1/warehouses/")
			.WithTags("Warehouses");

		return endpoints;
	}
}
using XmasSagas.Modules;

var builder = WebApplication.CreateBuilder(args);

// Register Modules
builder.RegisterModules();

var app = builder.Build();

app.UseCors("CorsPolicy");

// Register endpoints
app.MapEndpoints();

//app.UseEndpoints(routes =>
//{
//	routes.MapHub<XmasHub>("/hubs/xmas");
//});

// Configure the HTTP request pipeline.
app.UseSwagger(s =>
{
	s.RouteTemplate = "documentation/{documentName}/documentation.json";
});
app.UseSwaggerUI(s =>
{
	s.SwaggerEndpoint("/documentation/v1/documentation.json", "XmasDev Sagas");
	s.RoutePrefix = "documentation";
});

app.Run();
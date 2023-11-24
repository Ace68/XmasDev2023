using XmasLogistics.Modules;

var builder = WebApplication.CreateBuilder(args);

// Register Modules
builder.RegisterModules();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseAuthentication();

// Register endpoints
app.MapEndpoints();

// Configure the HTTP request pipeline.
app.UseSwagger(s =>
{
	s.RouteTemplate = "documentation/{documentName}/documentation.json";
});
app.UseSwaggerUI(s =>
{
	s.SwaggerEndpoint("/documentation/v1/documentation.json", "XmasDev Logistics");
	s.RoutePrefix = "documentation";
});

app.Run();
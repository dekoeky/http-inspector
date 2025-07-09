using HttpInspector;
using HttpInspector.Endpoints;
using HttpInspector.HealthChecks;
using Scalar.AspNetCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

var app = builder.Build();

// Defaults to /openapi/{documentName}.json with v1 as default documentName => /openapi/v1.json
app.MapOpenApi();

//Maps the scalar UI to /scalar
app.MapScalarApiReference();
app.MapDefaultHealthChecks();
app.MapInspector();
app.MapAbout();
app.MapBrowseEndpoints();

app.RegisterGettingStartedMessage();

app.Run();
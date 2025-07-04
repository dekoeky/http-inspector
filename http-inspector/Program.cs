using HttpInspector;
using HttpInspector.Endpoints;
using Scalar.AspNetCore;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.Services.AddOpenApi();

var app = builder.Build();

// Defaults to /openapi/{documentName}.json with v1 as default documentName => /openapi/v1.json
app.MapOpenApi();

//Maps the scalar UI to /scalar
app.MapScalarApiReference();
app.MapInspectorEndpoints();
app.MapAboutEndpoint();
app.MapBrowseEndpoints();

app.RegisterGettingStartedMessage();

app.Run();
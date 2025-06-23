using http_inspector.Endpoints;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, http_inspector.AppJsonSerializerContext.Default);
});

var app = builder.Build();

app.MapInspectorEndpoints();

app.Run();
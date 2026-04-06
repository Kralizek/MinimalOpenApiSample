var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.BookStore_Api>("bookstore-api");

builder.AddProject<Projects.BookStore_Web>("bookstore-web")
    .WithReference(api)
    .WithEnvironment("BOOKSTORE_API_URL", api.GetEndpoint("http"))
    .WithExternalHttpEndpoints()
    .WaitFor(api);

builder.Build().Run();

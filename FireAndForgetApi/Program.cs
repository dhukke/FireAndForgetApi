using FireAndForgetApi;
using FireAndForgetApi.Handlers;
using MediatR;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddFireAndForget();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/addWithDelay", async (ISender sender, ILoggerFactory loggerFactory) =>
{
    Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger("addWithDelay");

    logger.LogInformation("Received request to add item with delay.");

    await sender.Send(new AddItemCommand(Guid.NewGuid()));

    logger.LogInformation("Item added with delay. Request processed successfully.");

    return Results.Accepted();
})
.WithDescription("This endpoint adds an item with a delay using MediatR and publishes an event.")
.WithOpenApi();

app.MapPost("/addWithFireAndForget", async (ISender sender, ILoggerFactory loggerFactory) =>
{
    Microsoft.Extensions.Logging.ILogger logger = loggerFactory.CreateLogger("addWithDelay");

    logger.LogInformation("Received request to add item with fire-and-forget.");

    await sender.Send(new AddItemFireAndForgetCommand(Guid.NewGuid()));

    logger.LogInformation("Item added with fire-and-forget. Request processed successfully.");

    return Results.Accepted();
})
.WithDescription("This endpoint adds an item with fire-and-forget using IEventBus and publishes an event.")
.WithOpenApi();

app.Run();
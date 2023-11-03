using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Logging.AddOpenTelemetry(options =>
{
    var resBuilder = ResourceBuilder.CreateDefault()
        .AddService("OtlpWebApplication")
        .AddAttributes(new Dictionary<string, object>
        {
            ["SystemName"] = "LokiUsing" // see .\Dependencies\otel-collector-config.yml how to add 'SystemName' as a resource label
        });
    options.SetResourceBuilder(resBuilder);
    options.IncludeFormattedMessage = true;
    options.IncludeScopes = true;
    options.AddOtlpExporter(); // localhost:4317
});

// Alternative way to add OpenTelemetry exporter
//builder.Services.AddLogging(loggingBuilder =>
//    loggingBuilder.AddOpenTelemetry(options =>
//    {
//        do the same steps as above
//    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string serviceName = "OtlpWebApplication";
var resBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName)
    .AddAttributes(new Dictionary<string, object>
    {
        ["SystemName"] = "LokiUsing" // see .\Dependencies\otel-collector-config.yml how to add 'SystemName' as a resource label
    });


builder.Logging.AddOpenTelemetry(options =>
{
    options.IncludeFormattedMessage = true;
    options.IncludeScopes = true;

    options.SetResourceBuilder(resBuilder);

    options.AddOtlpExporter(); // localhost:4317
});

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

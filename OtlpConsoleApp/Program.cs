using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

const string serviceName = "OtlpConsoleApp";
var resBuilder = ResourceBuilder.CreateDefault()
    .AddService(serviceName)
    .AddAttributes(new Dictionary<string, object>
    {
        ["SystemName"] = "LokiUsing" // see .\Dependencies\otel-collector-config.yml how to add 'SystemName' as a resource label
    });

using var loggerFactory = LoggerFactory.Create(builder => builder
    .AddOpenTelemetry(options =>
    {
        options.IncludeFormattedMessage = true;
        options.IncludeScopes = true;

        options.SetResourceBuilder(resBuilder);

        options.AddOtlpExporter(); // localhost:4317
        options.AddConsoleExporter();
    }));

var logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Test message");
logger.LogWarning("Test warning");
logger.LogError("Test error");

GetWeatherForecast(logger);

Console.ReadLine();

static void GetWeatherForecast(ILogger logger1)
{
    using var client = new HttpClient();
    var response = client.GetAsync(new Uri("http://localhost:5125/WeatherForecast")).Result
        .Content.ReadAsStringAsync().Result;

    logger1.LogInformation("HTTP response {Response}", response);
}
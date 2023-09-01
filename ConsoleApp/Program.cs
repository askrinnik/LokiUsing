using LokiLoggingProvider.Options;
using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder => builder
    .AddLoki(configure =>
    {
        configure.Client = PushClient.Grpc;
        configure.StaticLabels.JobName = "LokiConsole";
        configure.StaticLabels.AdditionalStaticLabels.Add("SystemName", "LokiUsing");
    })
    .AddConsole());

var logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Test message");
logger.LogWarning("Test warning");
logger.LogError("Test error");

//GetWeatherForecast(logger);

Console.ReadLine();
return;

static void GetWeatherForecast(ILogger logger1)
{
    using var client = new HttpClient();
    var response = client.GetAsync(new Uri("http://localhost:5228/WeatherForecast")).Result
        .Content.ReadAsStringAsync().Result;

    logger1.LogInformation("HTTP response {Response}", response);
}
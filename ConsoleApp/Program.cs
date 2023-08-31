﻿using Microsoft.Extensions.Logging;

using var loggerFactory = LoggerFactory.Create(builder => builder
    //.AddLoki(configure =>
    //{
    //    configure.Client = PushClient.Grpc;
    //    configure.Grpc.Address = "http://localhost:9096";
    //    configure.StaticLabels.JobName = "LokiUsing4";
    //})
    .AddConsole());

var logger = loggerFactory.CreateLogger<Program>();

logger.LogInformation("Hello from my Console App!");
for (var i = 1; i < 10; i++)
    if(i%4 == 0)
      logger.LogError(i.ToString());
    else if (i%3 == 0)
        logger.LogWarning(i.ToString());
    else
        logger.LogInformation(i.ToString());

Console.ReadLine();
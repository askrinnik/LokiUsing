using LokiLoggingProvider.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.AddLoki(configure =>
{
    configure.Client = PushClient.Grpc;
    // configure.Grpc.Address = "http://localhost:9095";
    configure.StaticLabels.JobName = "LokiWebApplication";
    configure.StaticLabels.AdditionalStaticLabels.Add("SystemName", "LokiUsing");
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

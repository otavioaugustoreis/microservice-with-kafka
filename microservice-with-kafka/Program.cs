using Confluent.Kafka;
using microservice_with_kafka.Entities;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddSingleton<IProducer<string, string>>(sp =>
{
    var cfg = sp.GetRequiredService<IOptions<KafkaSettings>>().Value;
    var config = new ProducerConfig { BootstrapServers = cfg.BootstrapServers };
    return new ProducerBuilder<string, string>(config).Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

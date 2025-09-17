namespace microservice_with_kafka.Entities
{
    public class KafkaSettings
    {
        public string BootstrapServers { get; set; } = "";
        public string Topic { get; set; } = "";
    }
}

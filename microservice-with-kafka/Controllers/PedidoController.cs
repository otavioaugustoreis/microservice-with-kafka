using Confluent.Kafka;
using microservice_with_kafka.DTO;
using microservice_with_kafka.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace microservice_with_kafka.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PedidoController : ControllerBase
    {


        [HttpPost]
        public async Task<ActionResult> CriarPedido(IProducer<string, string> producer, IOptions<KafkaSettings> ks, PedidoDto pedido)
        {
            var topic = ks.Value.Topic;
            var key = pedido.Id.ToString();
            var value = System.Text.Json.JsonSerializer.Serialize(pedido);

            try
            {
                var result = await producer.ProduceAsync(topic, new Message<string, string> { Key = key, Value = value });
                return Ok(new { Partition = result.Partition.Value, Offset = result.Offset.Value });
            }
            catch (ProduceException<string, string> ex)
            {
                return Problem(ex.Error.Reason);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

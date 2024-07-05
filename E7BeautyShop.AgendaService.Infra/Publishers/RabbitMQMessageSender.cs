using System.Text;
using System.Text.Json;
using E7BeautyShop.AgendaService.Application.Interfaces;
using RabbitMQ.Client;

namespace E7BeautyShop.AgendaService.Infra.Publishers;

public class RabbitMqMessageSender(IConnection connection) : IBrokerMessageSender
{
    private readonly IConnection _connection = connection;

    public void SendMessage<T>(T message, string queueName)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: Body(message));
    }

    private static byte[] Body<T>(T message)
    {
        var json = JsonSerializer.Serialize(message);
        return Encoding.UTF8.GetBytes(json);
    }
}
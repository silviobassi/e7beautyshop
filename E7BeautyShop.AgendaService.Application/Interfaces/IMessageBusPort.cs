namespace E7BeautyShop.AgendaService.Application.Ports.Publishers;

public interface IMessageBusPort
{
    Task PublishMessageAsync<T>(T message, string queueName);
}
namespace E7BeautyShop.AgendaService.Application.Interfaces;

public interface IMessageBusPort
{
    Task PublishMessageAsync<T>(T message, string queueName);
}
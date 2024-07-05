using E7BeautyShop.AgendaService.Application.Interfaces;

namespace E7BeautyShop.AgendaService.Infra.Publishers;

public class MessageBus(IBrokerMessageSender messageSender) : IMessageBusPort
{
    public Task PublishMessageAsync<T>(T message, string queueName)
    {
        messageSender.SendMessage(message, queueName);
        return Task.CompletedTask;
    }
}
namespace E7BeautyShop.AgendaService.Application.Interfaces;

public interface IBrokerMessageSender
{
    void SendMessage<T>(T message, string queueName);
}
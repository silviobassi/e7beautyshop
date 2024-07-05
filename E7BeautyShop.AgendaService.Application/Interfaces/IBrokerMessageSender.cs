namespace E7BeautyShop.AgendaService.Application.Ports.Publishers;

public interface IBrokerMessageSender
{
    void SendMessage<T>(T message, string queueName);
}
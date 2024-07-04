﻿using E7BeautyShop.AgendaService.Application.Ports.Publishers;

namespace E7BeautyShop.AgendaService.Adapters.Outbound.Publishers;

public class MessageBus(IBrokerMessageSender messageSender) : IMessageBusPort
{
    public Task PublishMessageAsync<T>(T message, string queueName)
    {
        messageSender.SendMessage(message, queueName);
        return Task.CompletedTask;
    }
}
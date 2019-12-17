﻿using System.Threading.Tasks;

namespace SimulatedExchange.Messages
{
    public interface IMessageHandler<TMessage> where TMessage : IMessage
    {
        Task Handle(TMessage message);
    }
}

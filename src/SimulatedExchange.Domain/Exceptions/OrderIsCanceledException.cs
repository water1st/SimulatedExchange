using System;
using System.Runtime.Serialization;

namespace SimulatedExchange.Domain.Exceptions
{
    public class OrderIsCanceledException : Exception
    {
        public OrderIsCanceledException(string message) : base(message)
        {
        }

        public OrderIsCanceledException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OrderIsCanceledException()
        {
        }

        protected OrderIsCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

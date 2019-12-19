using System;
using System.Runtime.Serialization;

namespace SimulatedExchange.Domain.Exceptions
{
    public class OrderHasBeenDealException : Exception
    {
        public OrderHasBeenDealException(string message) : base(message)
        {
        }

        public OrderHasBeenDealException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OrderHasBeenDealException()
        {
        }

        protected OrderHasBeenDealException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

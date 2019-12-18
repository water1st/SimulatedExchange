using System;
using System.Runtime.Serialization;

namespace SimulatedExchange.Domain.Exceptions
{
    public class OrderIsFullTranscationException : Exception
    {
        public OrderIsFullTranscationException(string message) : base(message)
        {
        }

        public OrderIsFullTranscationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OrderIsFullTranscationException()
        {
        }

        protected OrderIsFullTranscationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

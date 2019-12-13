using System;
using System.Runtime.Serialization;

namespace SimulatedExchange.Exceptions
{
    public class UnregisteredWriterException : Exception
    {
        public UnregisteredWriterException(string message) : base(message)
        {
        }

        public UnregisteredWriterException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UnregisteredWriterException()
        {
        }

        protected UnregisteredWriterException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

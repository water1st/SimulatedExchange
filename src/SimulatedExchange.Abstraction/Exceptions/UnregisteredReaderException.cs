using System;
using System.Runtime.Serialization;

namespace SimulatedExchange.Exceptions
{
    public class UnregisteredReaderException : Exception
    {
        public UnregisteredReaderException(string message) : base(message)
        {
        }

        public UnregisteredReaderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UnregisteredReaderException()
        {
        }

        protected UnregisteredReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

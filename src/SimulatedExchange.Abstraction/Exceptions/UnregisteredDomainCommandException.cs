using System;
using System.Runtime.Serialization;

namespace SimulatedExchange.Exceptions
{
    public class UnregisteredDomainCommandException : Exception
    {
        public UnregisteredDomainCommandException(string message) : base(message)
        {
        }

        public UnregisteredDomainCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UnregisteredDomainCommandException()
        {
        }

        protected UnregisteredDomainCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

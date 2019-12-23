using System;
using System.Runtime.Serialization;

namespace SimulatedExchange.Domain.Exceptions
{
    public class InvalidPairSymbolsExcption : Exception
    {
        public InvalidPairSymbolsExcption()
        {
        }

        public InvalidPairSymbolsExcption(string message) : base(message)
        {
        }

        public InvalidPairSymbolsExcption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidPairSymbolsExcption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

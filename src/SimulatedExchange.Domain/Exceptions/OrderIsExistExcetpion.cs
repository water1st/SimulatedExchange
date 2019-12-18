using System;
using System.Runtime.Serialization;

namespace SimulatedExchange.Domain.Exceptions
{
    public class OrderIsExistExcetpion : Exception
    {
        public OrderIsExistExcetpion(string message) : base(message)
        {
        }

        public OrderIsExistExcetpion(string message, Exception innerException) : base(message, innerException)
        {
        }

        public OrderIsExistExcetpion()
        {
        }

        protected OrderIsExistExcetpion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;

namespace SimulatedExchange.Infrastructure
{
    public static class Converter
    {
        public static dynamic ChangeType(object obj, Type type)
        {
            return Convert.ChangeType(obj, type);
        }
    }
}

using System;

namespace SimulatedExchange.Domain.Orders.Entities
{
    public struct Currency : IEquatable<Currency>
    {
        private string name;

        public string Name { get => name?.ToUpper(); set => name = value?.ToUpper(); }

        public static implicit operator string(Currency currency) => currency.Name;

        public static implicit operator Currency(string currency) => new Currency { Name = currency };

        public static bool operator ==(Currency x, Currency y) => x.Equals(y);

        public static bool operator !=(Currency x, Currency y) => !x.Equals(y);

        public override bool Equals(object obj)
        {
            if (!ReferenceEquals(obj, null) && obj is Currency currency)
            {
                return Equals(currency);
            }

            return false;
        }

        public override int GetHashCode() => Name.GetHashCode();

        public bool Equals(Currency other)
        {
            if (!ReferenceEquals(other, null))
            {
                return other.GetHashCode() == GetHashCode();
            }

            return false;
        }

        public override string ToString() => Name;
    }
}

using System;

namespace SimulatedExchange.Domain.Orders.Entities
{
    public struct PairSymbols : IEquatable<PairSymbols>
    {
        public PairSymbols(Currency from, Currency to)
        {
            From = from;
            To = to;
        }

        public Currency From { get; set; }
        public Currency To { get; set; }

        public static implicit operator PairSymbols(string pairSymbol)
        {
            if (string.IsNullOrWhiteSpace(pairSymbol) || string.IsNullOrEmpty(pairSymbol))
            {
                throw new ArgumentException("message", nameof(pairSymbol));
            }

            var pair = pairSymbol.Split('-');
            if (pair.Length < 2)
            {
                throw new ArgumentException("pairSymbol does not contain '-'");
            }

            return new PairSymbols(pair[1], pair[0]);
        }

        public override bool Equals(object obj)
        {
            if (!ReferenceEquals(obj, null) && obj is PairSymbols other)
            {
                return Equals(other);
            }

            return false;
        }

        public bool Equals(PairSymbols other)
        {
            if (!ReferenceEquals(other, null))
            {
                return other.From == From && other.To == To;
            }

            return false;
        }

        public override int GetHashCode() => $"{From.GetHashCode()}-{To.GetHashCode()}".GetHashCode();

        public static bool operator ==(PairSymbols left, PairSymbols right) => left.Equals(right);

        public static bool operator !=(PairSymbols left, PairSymbols right) => !left.Equals(right);
    }
}

using System;

namespace GGuerra.Cardamatic.Encoding.Balance
{
    public sealed class Balance : IEquatable<Balance>
    {
        public Balance()
        {
            Value = 0;
        }

        public Balance(uint value)
        {
            Value = value;
        }

        public uint Value { get; }


        public bool Equals(Balance other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return other.Value.Equals(Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}

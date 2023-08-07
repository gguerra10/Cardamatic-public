using System;


namespace GGuerra.Cardamatic.Encoding.Date
{
    public sealed class Date : IEquatable<Date>
    {
        public Date(DateTime value)
        {
            Value = value;
        }

        public DateTime Value { get; }

        public bool Equals(Date other)
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
            return Value.ToString("yyyy/MM/dd");
        }

    }
}

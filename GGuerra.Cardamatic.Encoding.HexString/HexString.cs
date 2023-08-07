using System;


namespace GGuerra.Cardamatic.Encoding.HexString
{
    public sealed class HexString : IEquatable<HexString>
    {
        public HexString()
        {
            Value = string.Empty;
        }

        public HexString(string value)
        {
            Value = value;
        }

        public string Value { get; }


        public bool Equals(HexString other)
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

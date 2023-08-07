using System;


namespace GGuerra.Cardamatic.Encoding.BinaryString
{
    public sealed class BinaryString : IEquatable<BinaryString>
    {
        public BinaryString()
        {
            Value = string.Empty;
        }

        public BinaryString(string value)
        {
            Value = value;
        }

        public string Value { get; }


        public bool Equals(BinaryString other)
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

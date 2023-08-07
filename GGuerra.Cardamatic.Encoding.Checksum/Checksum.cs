using System;


namespace GGuerra.Cardamatic.Encoding.Checksum
{
    public sealed class Checksum : IEquatable<Checksum>
    {
        public Checksum()
        {
            Value = string.Empty;
        }

        public Checksum(string value)
        {
            Value = value;
        }

        public string Value { get; }


        public bool Equals(Checksum other)
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

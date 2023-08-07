using System;

namespace GGuerra.Cardamatic.Encoding.Time
{
    public sealed class Time : IEquatable<Time>
    {
        public Time(TimeSpan value)
        {
            Value = value;
        }

        public TimeSpan Value { get; }

        public bool Equals(Time other)
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
            return new DateTime(Value.Ticks).ToString("HH:mm:ss");
        }
    }
}

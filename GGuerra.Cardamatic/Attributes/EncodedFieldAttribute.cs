using System;

namespace GGuerra.Cardamatic.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EncodedFieldAttribute : Attribute
    {
        public uint Offset { get; }
        public uint Bits { get; }

        public EncodedFieldAttribute(uint offset, uint bits)
        {
            Offset = offset;
            Bits = bits;
        }
    }
}

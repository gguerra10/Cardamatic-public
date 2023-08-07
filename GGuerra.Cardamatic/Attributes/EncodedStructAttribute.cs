using System;


namespace GGuerra.Cardamatic.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class EncodedStructAttribute : Attribute
    {
        public uint Address { get; }

        public EncodedStructAttribute(uint address)
        {
            Address = address;
        }
    }
}

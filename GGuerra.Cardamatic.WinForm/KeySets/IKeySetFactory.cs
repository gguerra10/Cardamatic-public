using System.Collections.Generic;
using GGuerra.Cardamatic.CardReader.Interface.Facade;
using GGuerra.Cardamatic.WinForm.KeySets.Dto;


namespace GGuerra.Cardamatic.WinForm.KeySets
{
    public interface IKeySetFactory
    {
        void Initialize(string keySetPath, string keySetExtension);

        IEnumerable<KeySet> KeySets { get; }

    }
}

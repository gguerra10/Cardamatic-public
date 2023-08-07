using System;
using System.Collections.Generic;
using System.Text;

namespace GGuerra.Cardamatic.CardReader.Pcsc.Configuration
{
    public class PcscCardReaderConfiguration
    {
        public IDictionary<string, List<string>> Devices { get; set; }
    }
}

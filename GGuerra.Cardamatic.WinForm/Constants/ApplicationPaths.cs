using System;
using System.IO;

namespace GGuerra.Cardamatic.WinForm.Constants
{
    public static class ApplicationPaths
    {
        public static readonly string BasePath = "./";
        public static readonly string SchemasPath = Path.Combine(BasePath, "Schemas");
        public static readonly string KeysetsPath = Path.Combine(BasePath, "Keys");
        public static readonly string CardsPath = Path.Combine(BasePath, "Cards");

    }
}

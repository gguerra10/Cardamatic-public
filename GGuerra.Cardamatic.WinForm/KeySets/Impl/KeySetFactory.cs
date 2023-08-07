using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using GGuerra.Cardamatic.WinForm.KeySets.Dto;
using System.Security.Cryptography;

namespace GGuerra.Cardamatic.WinForm.KeySets.Impl
{
    public class KeySetFactory : IKeySetFactory
    {
        private readonly List<KeySet> _keySets;

        public KeySetFactory()
        {
            _keySets = new List<KeySet>();
        }

        public IEnumerable<KeySet> KeySets => _keySets;

        public void Initialize(string keySetPath, string keySetExtension)
        {
            // Iterate each file in provided path with provided extension.
            foreach (var filePath in Directory.EnumerateFiles(keySetPath, keySetExtension))
            {
                var keySet = GetKeySet(filePath);
                _keySets.Add(keySet);
            }
        }

        private KeySet GetKeySet(string keySetPath)
        {
            // Read data from file
            var encryptedDataFromFile = File.ReadAllBytes(keySetPath);

            // Decyphre data with ProtectedData
            var decryptedData = ProtectedData.Unprotect(encryptedDataFromFile, null, DataProtectionScope.LocalMachine);

            // Convert decyphered data to JSON string
            var decryptedJsonString = System.Text.Encoding.UTF8.GetString(decryptedData);

            // Deserialize JSON string to object
            var keySet = JsonConvert.DeserializeObject<KeySet>(decryptedJsonString);
            if (keySet == null)
            {
                throw new Exception($"File {keySetPath} is not a valid keyset.");
            }

            return keySet;
        }
    }
}

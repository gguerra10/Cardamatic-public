using GGuerra.Cardamatic.WinForm.KeySets.Dto;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;

namespace GGuerra.Cardamatic.KeysEncrypter
{
    class Program
    {
        private const string keyExtension = ".key";
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                var filePath = args[0];
                if (File.Exists(filePath))
                {
                    var fileName = Path.GetFileNameWithoutExtension(filePath);
                    var directory = Path.GetDirectoryName(filePath);

                    // Read file and check is valid keySet
                    var keySetContent = File.ReadAllText(filePath);
                    var keySet = JsonConvert.DeserializeObject<KeySet>(keySetContent);
                    if (keySet != null)
                    {
                        // Convert JSON string to byte array
                        var jsonData = System.Text.Encoding.UTF8.GetBytes(keySetContent);

                        // Cypher data with ProtectedData
                        var encryptedData = ProtectedData.Protect(jsonData, null, DataProtectionScope.LocalMachine);

                        // Write cyphered data to file
                        var outputFile = Path.Combine(directory, $"{fileName}{keyExtension}");
                        File.WriteAllBytes(outputFile, encryptedData);

                        Console.WriteLine($"File encrypted in {outputFile}");
                    }
                    else
                    {
                        Console.WriteLine($"File {filePath} is not a valid keyset.");
                    }
                }
                else
                {
                    Console.WriteLine($"File {filePath} does not exists.");
                }

            }
            else
            {
                Console.WriteLine("No parameter.");
            }
        }


    }
}

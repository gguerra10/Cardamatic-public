using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GGuerra.Cardamatic.Encoding.Interface.Facade;


namespace GGuerra.Cardamatic.Encoding.Facade.Impl
{
    public class DecoderManager : IDecoderManager
    {
        private readonly IEnumerable<IDecodable> _Decodables;
        private readonly IEnumerable<IEncodable> _encodables;
        private readonly IEnumerable<IParseable> _parseables;

        public DecoderManager(
            IEnumerable<IDecodable> Decodables,
            IEnumerable<IEncodable> encodables,
            IEnumerable<IParseable> parseables
            )
        {
            _Decodables = Decodables;
            _encodables = encodables;
            _parseables = parseables;
        }

        public IDecodable GetDecodable(string typeName)
        {
            var type = GetAssemblyNameContainingType(typeName);
            var decoder = GetDecodable(type);
            return decoder;
        }


        public IEncodable GetEncodable(string typeName)
        {
            var type = GetAssemblyNameContainingType(typeName);
            var decoder = GetEncodable(type);
            return decoder;
        }

        public IParseable GetParseable(string typeName)
        {
            var type = GetAssemblyNameContainingType(typeName);
            var parseable = GetParseable(type);
            return parseable;
        }

        private static Type GetAssemblyNameContainingType(string typeName)
        {
            Type type = null;
            foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = currentassembly.GetType(typeName, false, true);
                if (type != null)
                {
                    return type;
                }
            }
            if (type == null)
            {
                throw new NotImplementedException($"Type {typeName} not found");
            }
            return type;
        }

        private IDecodable GetDecodable(Type type)
        {
            var Decodable = _Decodables.FirstOrDefault(d => d.Type == type);
            if (Decodable == null)
            {
                throw new NotImplementedException($"Decodable for type {type.Name} not found.");
            }
            return Decodable;
        }

        private IEncodable GetEncodable(Type type)
        {
            var encodable = _encodables.FirstOrDefault(d => d.Type == type);
            if (encodable == null)
            {
                throw new NotImplementedException($"Encodable for type {type.Name} not found.");
            }
            return encodable;
        }

        private IParseable GetParseable(Type type)
        {
            var parseable = _parseables.FirstOrDefault(d => d.Type == type);
            if (parseable == null)
            {
                throw new NotImplementedException($"Parseable for type {type.Name} not found.");
            }
            return parseable;
        }
    }
}

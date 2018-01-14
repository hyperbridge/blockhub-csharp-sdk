using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub.Data.FileSystem
{
    public class FileSystemJsonLoader<T> : ILoader<T>
    {
        public async Task<T> Load(string uri)
        {
            var parsedUri = new Uri(uri);
            if (!parsedUri.Scheme.Equals("file", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Cannot load this scheme type.");

            var filePath = parsedUri.LocalPath;
            if (!File.Exists(filePath)) throw new FileNotFoundException("File not found.");

            var json = File.ReadAllText(filePath);

            // TODO: If the future shows we need to serialize differently, we should extract
            //       serialization/deserialization into other interfaces.
            return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings
            {
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                TypeNameHandling = TypeNameHandling.All
            });
        }
    }
}

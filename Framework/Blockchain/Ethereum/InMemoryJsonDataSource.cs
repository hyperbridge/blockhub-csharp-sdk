using System;
using System.IO;

namespace Hyperbridge.Blockchain.Ethereum 
{
    public class InMemoryJsonDataSource : IJsonDataSource 
    {
        private string Json { get; }

        public InMemoryJsonDataSource(string json)
        {
            if (json == null) throw new ArgumentNullException(nameof(json));
            this.Json = json;
        }

        public string GetData() {
            return json;
        }
    }
}
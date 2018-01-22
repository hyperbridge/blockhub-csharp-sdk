using Blockhub.Data;
using System;
using System.Threading.Tasks;

namespace Blockhub.Tests
{
    class EmulateLoad<T> : ILoad<T>
    {
        private Func<string, T> Loader { get; }

        public EmulateLoad(Func<string, T> loader)
        {
            Loader = loader ?? throw new ArgumentNullException(nameof(loader));
        }

        public Task<T> Load(string uri)
        {
            return Task.FromResult(Loader(uri));
        }
    }
}

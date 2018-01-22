using Blockhub.Wallet;
using System.Threading.Tasks;

namespace Blockhub.Tests
{
    class EmulatePrivateKeyGenerator<T> : IPrivateKeyGenerate<T>
        where T : IBlockchainType
    {
        private readonly string Key;

        public EmulatePrivateKeyGenerator(string key)
        {
            Key = key;
        }

        public Task<string> GetPrivateKey(Wallet<T> wallet, string address)
        {
            return Task.FromResult(Key);
        }
    }
}

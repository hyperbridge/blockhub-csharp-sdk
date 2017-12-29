using System;

namespace Hyperbridge.Blockchain.Ethereum
{
    public class JsonDataSourceAccountLoader : IAccountLoader 
    {
        private Nethereum.KeyStore.KeyStoreService KeyStoreService { get; }

        public JsonDataSourceAccountLoader(Nethereum.KeyStore.KeyStoreService keyStoreService)
        {
            if (keyStoreService == null) throw new ArgumentNullException(nameof(keyStoreService));
            this.KeyStoreService = keyStoreService;
        }

        public Account Load(IJsonDataSource dataSource, string password) {
            var json = dataSource.GetData();
            var key = KeyStoreService.DecryptKeyStoreFromJson(password, json);

            return new Account(key);
        }
    }
}
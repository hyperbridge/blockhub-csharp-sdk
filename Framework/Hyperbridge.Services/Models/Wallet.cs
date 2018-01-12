using Hyperbridge.Services.Abstract;
using Hyperbridge.Wallet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Hyperbridge.Services
{
    public class Wallet
    {
        [JsonProperty]
        [JsonConverter(typeof(ICoinCurrencyConverter))]
        public ICoinCurrency BlockchainType { get; internal set; }

        [JsonProperty]
        public string Id { get; internal set; }

        [JsonProperty]
        public string Name { get; internal set; }

        [JsonProperty]
        public string Secret { get; internal set; }

        // In order to properly create new accounts without re-using any that have already
        // been created in the past, we should keep track of the last index used to create
        // an account.
        [JsonProperty]
        public int LastIndexUsed { get; internal set; }
        // NOTE: Do not store password at all anywhere

        [JsonProperty]
        public List<Account> Accounts { get; } = new List<Account>();
    }

    public class InMemoryWalletCreator : IWalletCreator
    {
        private ISeedGenerator<string> Generator { get; }

        public InMemoryWalletCreator(ICoinCurrency currency, ISeedGenerator<string> generator)
        {
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            Generator = generator ?? throw new ArgumentNullException(nameof(generator));
        }

        public ICoinCurrency Currency { get; }
        public Wallet CreateWallet(string secret, string name, string password)
        {
            return new Wallet
            {
                BlockchainType = Currency,
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Secret = secret,
                LastIndexUsed = 0
            };
        }

        public Wallet CreateWallet(string name, string password)
        {
            return CreateWallet(Generator.Generate(), name, password);
        }
    }
}

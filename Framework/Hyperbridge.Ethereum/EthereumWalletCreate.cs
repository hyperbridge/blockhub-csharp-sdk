﻿using System;
using Blockhub.Wallet;
using Blockhub.Services.Abstract;
using System.Threading.Tasks;
using Blockhub.Ethereum;
using System.Linq;
using Blockhub.Services;

namespace Blockhub.Ethereum
{
    public class EthereumWalletCreate : IWalletCreate<Ethereum>
    {
        private ISeedGenerator<string> SeedGenerator { get; }
        public EthereumWalletCreate(ISeedGenerator<string> seedGenerator)
        {
            SeedGenerator = seedGenerator ?? throw new ArgumentNullException(nameof(seedGenerator));
        }

        public async Task<Wallet<Ethereum>> CreateWallet(string secret, string name, string password)
        {
            return new Wallet<Ethereum>
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Secret = secret
            };
        }

        public Task<Wallet<Ethereum>> CreateWallet(string name, string password)
        {
            var seed = SeedGenerator.Generate();
            return CreateWallet(seed, name, password);
        }
    }
}

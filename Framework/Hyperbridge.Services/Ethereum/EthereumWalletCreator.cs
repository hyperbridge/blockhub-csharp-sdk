﻿using Hyperbridge.Ethereum;
using Hyperbridge.Services.Abstract;
using Hyperbridge.Wallet;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hyperbridge.Services.Ethereum
{
    public class EthereumWalletCreator : IWalletCreator
    {
        public ICoinCurrency Currency => Ether.Instance;
        public Wallet CreateWallet(string secret, string name, string password)
        {
            // NOTE: Password isn't being used right now.
            return new Wallet
            {
                BlockchainType = Ether.Instance,
                Id = Guid.NewGuid().ToString(),
                LastIndexUsed = -1,
                Name = name,

                // Could potentially use password to encrypt password here, but this will require
                // the password to create accounts
                Secret = secret
            };
        }
    }
}

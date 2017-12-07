using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.Blocks;
using System.Threading.Tasks;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Signer;

namespace Hyperbridge.Blockchain.Ethereum
{
    public class Account
    {
        public string Address { get; protected set; }
        public string PrivateKey { get; protected set; }

#if !PCL
        public static Account LoadFromKeyStoreFile(string filePath, string password)
        {
            var keyStoreService = new Nethereum.KeyStore.KeyStoreService();
            var key = keyStoreService.DecryptKeyStoreFromFile(password, filePath);

            return new Account(key);
        }
#endif

        public static Account LoadFromKeyStore(string json, string password)
        {
            var keyStoreService = new Nethereum.KeyStore.KeyStoreService();
            var key = keyStoreService.DecryptKeyStoreFromJson(password, json);

            return new Account(key);
        }

        public Account(EthECKey key)
        {
            this.Initialize(key);
        }

        public Account(string privateKey)
        {
            this.Initialize(new EthECKey(privateKey));
        }

        public Account(byte[] privateKey)
        {
            this.Initialize(new EthECKey(privateKey, true));
        }

        private void Initialize(EthECKey key)
        {
            this.PrivateKey = key.GetPrivateKey();
            this.Address = key.GetPublicAddress();
        }
    }
}
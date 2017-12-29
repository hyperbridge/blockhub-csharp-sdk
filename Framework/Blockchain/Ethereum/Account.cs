using System;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.Blocks;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Signer;

namespace Hyperbridge.Blockchain.Ethereum
{
    public class Account
    {
        public string Address { get; }
        public string PrivateKey { get; }   // Should the private key really be public?

        public Account(EthECKey key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));

            this.PrivateKey = key.GetPrivateKey();
            this.Address = key.GetPublicAddress();

            // What error should we use here?
            if (this.PrivateKey == null) throw new Exception();
            if (this.Address == null) throw new Exception();
        }

        public Account(string privateKey) : this(new EthECKey(privateKey)) { }
        public Account(byte[] privateKey) : this(new EthECKey(privateKey, true)) { }
    }
}
using System;
using Blockhub.Transaction;
using Blockhub.Wallet;
using NBitcoin.BouncyCastle.Math;
using Newtonsoft.Json;

namespace Blockhub.Ethereum
{
    public class EthereumTransaction : ITransaction
    {
        public DateTime TransactionTime { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public Wei Coin { get; set; }

        public BigInteger GetAmount()
        {
            return Coin.ToTransactionAmount();
        }

        public ulong BlockNumber { get; set; }
        public string BlockHash { get; set; }
        public ulong TransactionIndex { get; set; }
        public string TransactionHash { get; set; }

        public ulong Gas { get; set; }
        public ulong GasPrice { get; set; }
        public ulong CumulativeGasUsed { get; set; }
        public ulong GasUsed { get; set; }
        public ulong NumberConfirmations { get; set; }

        public string ContractAddress { get; set; }
        public string Input { get; set; }
    }
}

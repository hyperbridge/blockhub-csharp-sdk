using Hyperbridge.Ethereum;
using Hyperbridge.Services.Abstract;
using Hyperbridge.Transaction;
using Hyperbridge.Wallet;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace Hyperbridge.Services.Ethereum
{
    public class EthereumTransactionWrite : ITransactionWrite
    {
        private ITransactionWrite<Ether> TransactionWriter { get; }
        public EthereumTransactionWrite(ITransactionWrite<Ether> transactionWriter)
        {
            TransactionWriter = transactionWriter ?? throw new ArgumentNullException(nameof(transactionWriter));
        }

        public async Task SendTransaction(SendTransaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (transaction.Currency == null || transaction.Currency != Ether.Instance) throw new InvalidOperationException("Must be Ether currency.");
            if (transaction.FromAddress == null) throw new InvalidOperationException("Must specify the payer.");
            if (string.IsNullOrWhiteSpace(transaction.ToAddress)) throw new InvalidOperationException("Must specify the recipient.");

            // From Address
            var ethWallet = new Nethereum.NethereumHdWallet(transaction.FromAddress.Wallet.Secret);
            var fromAccount = ethWallet.GetAccount(transaction.FromAddress.WalletIndex);

            // To Address
            var toAccount = new ToEthereumAccount(transaction.ToAddress);

            // Payment
            ICoin<Ether> amount = new EtherCoin(0M);

            if (transaction.Amount > 0M)
            {
                switch (transaction.Unit.ToUpper().Trim())
                {
                    case "ETH":
                        amount = new EtherCoin(transaction.Amount);
                        break;
                    case "WEI":
                        amount = new WeiCoin((BigInteger)transaction.Amount);
                        break;
                    default:
                        throw new InvalidOperationException("Unit must be Ether (ETH) or Wei (WEI).");
                }
            }


            await TransactionWriter.SendTransactionAsync(fromAccount, toAccount, amount);
        }
    }
}

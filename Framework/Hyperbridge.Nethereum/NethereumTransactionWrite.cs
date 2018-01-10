using Hyperbridge.Wallet;
using Hyperbridge.Ethereum;
using Hyperbridge.Transaction;
using System.Threading.Tasks;
using System;
using N = Nethereum.Web3;
using H = Nethereum.Hex.HexTypes;

namespace Hyperbridge.Nethereum
{
    public class NethereumTransactionWrite : ITransactionWrite<Ether>
    {
        private string Url { get; }

        public NethereumTransactionWrite(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            Url = url;
        }

        public async Task<TransactionSentResponse<Ether>> SendTransactionAsync(IAccount<Ether> fromAccount, IAccount<Ether> toAccount, ICoin<Ether> amount)
        {
            var wei = amount.ToTransactionAmount();

            var client = GetClient(fromAccount);
            var hash = await client.TransactionManager.SendTransactionAsync(fromAccount.Address, toAccount.Address, new H.HexBigInteger(wei));

            // TODO: Can we ask ask for the Transaction Receipt immediately or do we have to wait? Do we even need it?
            return new TransactionSentResponse<Ether>(fromAccount, toAccount, amount, hash);
        }

        private N.Web3 GetClient(IAccount<Ether> fromAccount)
        {
            var account = new N.Accounts.Account(fromAccount.PrivateKey);
            return new N.Web3(account, Url);
        }
    }
}

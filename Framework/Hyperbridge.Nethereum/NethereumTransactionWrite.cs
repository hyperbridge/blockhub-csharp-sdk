using Blockhub.Wallet;
using Blockhub.Ethereum;
using Blockhub.Transaction;
using System.Threading.Tasks;
using System;
using N = Nethereum.Web3;
using H = Nethereum.Hex.HexTypes;

namespace Blockhub.Nethereum
{
    public class NethereumTransactionWrite : ITransactionWrite<Ethereum.Ethereum>
    {
        private string Url { get; }

        public NethereumTransactionWrite(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            Url = url;
        }

        public async Task<TransactionSentResponse<Ethereum.Ethereum>> SendTransactionAsync(IAccount<Ethereum.Ethereum> fromAccount, string toAddress, IToken<Ethereum.Ethereum> amount)
        {
            var wei = amount.ToTransactionAmount();

            var client = GetClient(fromAccount);
            var hash = await client.TransactionManager.SendTransactionAsync(fromAccount.Address, toAddress, new H.HexBigInteger(wei));

            // TODO: Can we ask ask for the Transaction Receipt immediately or do we have to wait? Do we even need it?
            return new TransactionSentResponse<Ethereum.Ethereum>(fromAccount, toAddress, amount, hash);
        }

        private N.Web3 GetClient(IAccount<Ethereum.Ethereum> fromAccount)
        {
            var account = new N.Accounts.Account(fromAccount.PrivateKey);
            return new N.Web3(account, Url);
        }
    }
}

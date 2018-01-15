using Blockhub.Wallet;
using Blockhub.Ethereum;
using Blockhub.Transaction;
using System.Threading.Tasks;
using System;
using N = Nethereum.Web3;
using H = Nethereum.Hex.HexTypes;
using Blockhub.Services;

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

        public async Task<TransactionSentResponse<Ethereum.Ethereum>> SendTransactionAsync(Account<Ethereum.Ethereum> fromAccount, string toAddress, IToken<Ethereum.Ethereum> amount)
        {
            var wei = amount.ToTransactionAmount();

            var client = GetClient(fromAccount);
            var hash = await client.TransactionManager.SendTransactionAsync(fromAccount.Address, toAddress, new H.HexBigInteger(wei.ToString()));

            // TODO: Can we ask ask for the Transaction Receipt immediately or do we have to wait? Do we even need it?
            return new TransactionSentResponse<Ethereum.Ethereum>(fromAccount, toAddress, amount, hash);
        }

        private N.Web3 GetClient(Account<Ethereum.Ethereum> fromAccount)
        {
            var privateKey = fromAccount.GetPrivateKey();
            if (string.IsNullOrWhiteSpace(privateKey)) throw new InvalidPrivateKeyException<Ethereum.Ethereum>(fromAccount);

            var account = new N.Accounts.Account(privateKey);
            return new N.Web3(account, Url);
        }
    }
}

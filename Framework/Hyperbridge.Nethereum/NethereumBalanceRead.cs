using System;
using Blockhub.Wallet;
using Blockhub.Ethereum;
using System.Threading.Tasks;
using N = Nethereum.Web3;

namespace Blockhub.Nethereum
{
    public class NethereumBalanceRead : IBalanceRead<Ethereum.Ethereum>
    {
        private string Url { get; }

        public NethereumBalanceRead(string url)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            Url = url;
        }

        public async Task<IToken<Ethereum.Ethereum>> GetBalance(IAccount<Ethereum.Ethereum> account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));

            var client = GetClient(account);
            var balance = await client.Eth.GetBalance.SendRequestAsync(account.Address);
            decimal ether = N.Web3.Convert.FromWei(balance);

            return new EtherCoin(ether);
        }

        private N.Web3 GetClient(IAccount<Ethereum.Ethereum> account)
        {
            var nethereumAccount = new N.Accounts.Account(account.PrivateKey);
            return new N.Web3(nethereumAccount, Url);
        }
    }
}

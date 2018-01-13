using Hyperbridge.Ethereum;
using Hyperbridge.Services.Abstract;
using Hyperbridge.Wallet;
using System;
using System.Threading.Tasks;

namespace Hyperbridge.Services.Ethereum
{
    public class EthereumAccountBalanceReader : IAccountBalanceReader
    {
        private IBalanceRead<Ether> BalanceReader { get; }
        public EthereumAccountBalanceReader(IBalanceRead<Ether> balanceReader)
        {
            BalanceReader = balanceReader ?? throw new ArgumentNullException(nameof(balanceReader));
        }

        public async Task<AccountBalance> GetAccountBalance(Account account)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (account.Wallet.BlockchainType != Ether.Instance) throw new InvalidOperationException("Account must be an Ethereum account.");

            var ethWallet = new Nethereum.NethereumHdWallet(account.Wallet.Secret);
            var ethAccount = ethWallet.GetAccount(account.WalletIndex);

            // This conversion is handled by utilizing implicit/explicit operators
            var balance = (EtherCoin) await BalanceReader.GetBalance(ethAccount);

            return new AccountBalance
            {
                Amount = balance.Amount,
                Unit = balance.Unit
            };
        }
    }
}

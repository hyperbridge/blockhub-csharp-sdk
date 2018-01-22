using Blockhub.Wallet;
using System.Threading.Tasks;

namespace Blockhub.Tests
{
    class EmulateAccountCreate<T> : IAccountCreate<T>
        where T : IBlockchainType
    {
        public EmulateAccountCreate(Account<T> account)
        {
            ReturnAccount = account;
        }

        public Account<T> ReturnAccount { get; }

        public Task<Account<T>> CreateAccount(Wallet<T> wallet, string address)
        {
            return Task.FromResult(ReturnAccount);
        }

        public Task<Account<T>> CreateAccount(Wallet<T> wallet, string address, string name)
        {
            return Task.FromResult(ReturnAccount);
        }

        public Task<Account<T>> CreateAccount(Wallet<T> wallet, int index)
        {
            return Task.FromResult(ReturnAccount);
        }

        public Task<Account<T>> CreateAccount(Wallet<T> wallet, int index, string name)
        {
            return Task.FromResult(ReturnAccount);
        }

        public Task<Account<T>[]> CreateAccounts(Wallet<T> wallet, int count, int startIndex = 0)
        {
            return Task.FromResult(new Account<T>[] { ReturnAccount });
        }
    }
}

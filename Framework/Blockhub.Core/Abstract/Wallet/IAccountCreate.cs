using Blockhub.Services;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    public interface IAccountCreate<T> where T : IBlockchainType
    {
        /// <summary>
        /// Get an account by the address provided. An WalletAddressNotFoundException is thrown when it was not found.
        /// </summary>
        /// <param name="address">The address to find in the wallet.</param>
        /// <returns>The found account</returns>
        Task<Account<T>> CreateAccount(Wallet<T> wallet, string address);
        Task<Account<T>> CreateAccount(Wallet<T> wallet, string address, string name);

        /// <summary>
        /// Get an account based on the index in the HD Wallet.
        /// </summary>
        /// <param name="index">The index of the account.</param>
        /// <returns>The found account</returns>
        Task<Account<T>> CreateAccount(Wallet<T> wallet, int index);
        Task<Account<T>> CreateAccount(Wallet<T> wallet, int index, string name);

        /// <summary>
        /// Generate a number of accounts for the wallet and optionally at a start index.
        /// </summary>
        /// <param name="count">The number of accounts to generate.</param>
        /// <param name="startIndex">The starting index for generating accounts.</param>
        /// <returns></returns>
        Task<Account<T>[]> CreateAccounts(Wallet<T> wallet, int count, int startIndex = 0);
    }
}

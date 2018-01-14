using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockhub.Wallet
{
    /// <summary>
    /// General blockchain wallet used to generate (or find) accounts.
    /// </summary>
    /// <typeparam name="T">Blockchain account type</typeparam>
    public interface IIndexedWalletManage<T> where T : ITokenSource
    {
        /// <summary>
        /// Get an account by the address provided. An WalletAddressNotFoundException is thrown when it was not found.
        /// </summary>
        /// <param name="address">The address to find in the wallet.</param>
        /// <returns>The found account</returns>
        Task<IAccount<T>> GetAccount(IWallet<T> wallet, string address);

        /// <summary>
        /// Get an account based on the index in the HD Wallet.
        /// </summary>
        /// <param name="index">The index of the account.</param>
        /// <returns>The found account</returns>
        Task<IAccount<T>> GetAccount(IWallet<T> wallet, int index);

        /// <summary>
        /// Generate a number of accounts for the wallet and optionally at a start index.
        /// </summary>
        /// <param name="count">The number of accounts to generate.</param>
        /// <param name="startIndex">The starting index for generating accounts.</param>
        /// <returns></returns>
        Task<IAccount<T>[]> GenerateAccounts(IWallet<T> wallet, int count, int startIndex = 0);
    }
}

using Blockhub.Wallet;
using System;
using System.Threading.Tasks;

namespace Blockhub.Transaction
{
    public class LoadMissingPrivateKeyTransactionWrite<T> : ITransactionWrite<T>
        where T : ITokenSource
    {
        private ITransactionWrite<T> TransactionWriter { get; }
        private IIndexedWalletManage<T> WalletManager { get; }
        public LoadMissingPrivateKeyTransactionWrite(ITransactionWrite<T> transactionWriter, IIndexedWalletManage<T> walletManager)
        {
            TransactionWriter = transactionWriter ?? throw new ArgumentNullException(nameof(transactionWriter));
            WalletManager = walletManager ?? throw new ArgumentNullException(nameof(walletManager));
        }

        public async Task<TransactionSentResponse<T>> SendTransactionAsync(Account<T> fromAccount, string toAddress, IToken<T> amount)
        {
            if (string.IsNullOrWhiteSpace(fromAccount.GetPrivateKey()))
            {
                fromAccount.SetPrivateKey(await WalletManager.GetPrivateKey(fromAccount.Wallet, fromAccount.Address));
            }

            return await TransactionWriter.SendTransactionAsync(fromAccount, toAddress, amount);
        }
    }
}

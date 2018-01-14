using Blockhub.Wallet;
using System;
using System.Threading.Tasks;

namespace Blockhub.Transaction
{
    public class LoadMissingPrivateKeyTransactionWrite<T> : ITransactionWrite<T>
        where T : ITokenSource
    {
        private ITransactionWrite<T> TransactionWriter { get; }
        private IPrivateKeyGenerate<T> PrivateKeyGenerator { get; }
        public LoadMissingPrivateKeyTransactionWrite(ITransactionWrite<T> transactionWriter, IPrivateKeyGenerate<T> privateKeyGenerator)
        {
            TransactionWriter = transactionWriter ?? throw new ArgumentNullException(nameof(transactionWriter));
            PrivateKeyGenerator = privateKeyGenerator ?? throw new ArgumentNullException(nameof(privateKeyGenerator));
        }

        public async Task<TransactionSentResponse<T>> SendTransactionAsync(Account<T> fromAccount, string toAddress, IToken<T> amount)
        {
            if (string.IsNullOrWhiteSpace(fromAccount.GetPrivateKey()))
            {
                fromAccount.SetPrivateKey(await PrivateKeyGenerator.GetPrivateKey(fromAccount.Wallet, fromAccount.Address));
            }

            return await TransactionWriter.SendTransactionAsync(fromAccount, toAddress, amount);
        }
    }
}

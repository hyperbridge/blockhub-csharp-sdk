using Blockhub;
using Blockhub.Ethereum;
using Blockhub.Transaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using NBitcoin.BouncyCastle.Math;

public class GetLastTransactionButton : BaseButton
{
    [Inject]
    public ILastTransactionRead<EthereumTransaction> TransactionReader { get; set; }

    [Inject]
    public IProfileContextAccess ProfileAccessor { get; set; }

    protected override async void OnButtonClick()
    {
        try
        {
            var profile = ProfileAccessor.Profile;
            if (profile == null)
            {
                Message("Profile not set.");
                return;
            }

            var wallet = profile.GetWallets<Ethereum>()?.FirstOrDefault();
            if (wallet == null)
            {
                Message("Wallet not in the profile.");
                return;
            }

            var account = wallet.Accounts.FirstOrDefault();
            if (account == null)
            {
                Message("No accounts in the wallet.");
                return;
            }

            var transactions = await TransactionReader.GetLastTransactions(account.Address, 1, 1);
            var t = transactions.FirstOrDefault();
            if (!transactions.Any())
            {
                Message("No transactions found.");
            }
            else
            {
                Message($"Last Transaction: TimeStamp = {t.TransactionTime}, To = {t.ToAddress}, Amount = {t.GetAmount()} WEI");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
}

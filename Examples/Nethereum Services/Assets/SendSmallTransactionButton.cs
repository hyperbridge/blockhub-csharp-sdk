using Blockhub;
using Blockhub.Ethereum;
using Blockhub.Transaction;
using Blockhub.Wallet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SendSmallTransactionButton : BaseButton {
    [Inject]
    public ITransactionWrite<Ethereum> TransactionWriter { get; set; }

    [Inject]
    public IAccountCreate<Ethereum> AccountCreator { get; set; }

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

            Account<Ethereum> toAccount = null;
            if (wallet.Accounts.Count() == 1)
            {
                toAccount = await AccountCreator.CreateAccount(wallet, 1, "Account 2");
            } else
            {
                toAccount = wallet.Accounts.ElementAt(1);
            }

            var response = await TransactionWriter.SendTransactionAsync(account, toAccount.Address, new WeiCoin(new NBitcoin.BouncyCastle.Math.BigInteger("100")));
            Message($"Transaction Successful! Transaction Hash = {response.TransactionHash}");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
}

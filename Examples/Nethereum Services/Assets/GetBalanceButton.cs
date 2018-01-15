using Blockhub;
using Blockhub.Ethereum;
using Blockhub.Wallet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class GetBalanceButton : BaseButton
{
    [Inject]
    public IBalanceRead<Ethereum> BalanceReader { get; set; }

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

            var balance = await BalanceReader.GetBalance(account);
            Message($"Account Balance = {balance.ToDisplayAmount()}");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
}

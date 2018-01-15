using Blockhub;
using Blockhub.Ethereum;
using Blockhub.Wallet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class CreateAccountButton : BaseButton {
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

            var account = await AccountCreator.CreateAccount(wallet, 0, "Main");
            if (account == null)
            {
                Message("Account not created.");
                return;
            }

            Message($"Account Created! Address = {account.Address}, Private Key = {account.GetPrivateKey()}");
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
}

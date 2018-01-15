using Blockhub.Ethereum;
using Blockhub.Wallet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class CreateWalletButton : BaseButton {
    [Inject]
    public IWalletCreate<Ethereum> WalletCreator { get; set; }

    protected override async void OnButtonClick()
    {
        const string MNEMONIC_PHRASE = "candy maple cake sugar pudding cream honey rich smooth crumble sweet treat";

        try
        {
            Debug.Log("Creating Wallet... (Checking Balances)");
            var wallet = await WalletCreator.CreateWallet(MNEMONIC_PHRASE, "Test Wallet", "");
            Assert.IsNotNull(wallet, "Wallet is null.");

            Message($"Wallet was successfully create [Id = {wallet.Id}].");
        } catch(Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
}

using Blockhub.Ethereum;
using Blockhub.Wallet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using Zenject;

public class CreateWalletButton : BaseButton {
    [Inject]
    public IWalletCreate<Ethereum> WalletCreator { get; set; }

    [Inject]
    public IAccountSearcher<Ethereum> AccountSearcher { get; set; }

    [Inject]
    public IBalanceRead<Ethereum> BalanceReader { get; set; }

    protected override async void OnButtonClick()
    {
        const string MNEMONIC_PHRASE = "candy maple cake sugar pudding cream honey rich smooth crumble sweet treat";

        try
        {
            Debug.Log("Creating Wallet... (Checking Balances)");
            var wallet = await WalletCreator.CreateWallet(MNEMONIC_PHRASE, "Test Wallet", "");
            Assert.IsNotNull(wallet, "Wallet is null.");

            // Search for Accounts
            var foundAccounts = await AccountSearcher.SearchForAccounts(wallet, 10);
            Debug.Log($"Found Account Count: {foundAccounts.Count()}");

            foreach (var account in foundAccounts)
            {
                var balance = await BalanceReader.GetBalance(account);
                Debug.Log($"Found Account: Balance = {balance}, Address = {account.Address}");
                wallet.Accounts.Add(account);
            }

            Message($"Wallet was successfully create [Id = {wallet.Id}, # Accounts = {wallet.Accounts.Count}].");
        } catch(Exception ex)
        {
            Debug.LogError(ex.ToString());
        }
    }
}

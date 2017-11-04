using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.Blocks;
using System.Threading.Tasks;
using Nethereum.RPC.TransactionManagers;
using Nethereum.Signer;
using UnityEngine.UI;
using SFB;

public class WalletService : MonoBehaviour
{
    public InputField nameField, passwordField;
    public Text validationText;

    string _path, keystore;

    private void Start()
    {
        _path = "";

    }

    public bool CorrectWalletInfo()
    {
        if (nameField.text.Length < 1 || passwordField.text.Length < 7)
        {
            validationText.text = "Empty Name or Incomplete Password";
            return false;
        }
        else
        {
            return true;
        }
    }

    public void FindKeystore()
    {
        _path = "";

        WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false));
        StreamReader reader = new StreamReader(_path);
        keystore = reader.ReadToEnd();
        reader.Close();
        KeystoreValidate(keystore);
    }

    public void AcceptWallet()
    {
        if (KeystoreValidate(keystore) && CorrectWalletInfo())
        {
            StartCoroutine(ConfirmAccount());
        }
    }

    public IEnumerator ConfirmAccount()
    {
        Nethereum.KeyStore.KeyStoreService keyStoreService = new Nethereum.KeyStore.KeyStoreService();
        byte[] key = keyStoreService.DecryptKeyStoreFromJson(passwordField.text, keystore);

        Account account = new Account(key);
        //Checking no other wallets have the same address, because we'd have a duplicate

        foreach (WalletInfo wallet in AppManager.instance.walletManager.wallets)
        {
            if(wallet.address == account.Address)
            {
                validationText.text = "Another wallet with this same address exists";
                yield break;
            }
        }

        Debug.Log(account.Address);
        Debug.Log(account.PrivateKey);

        WalletInfo newWallet = new WalletInfo();
        
        newWallet.SetupWallet(Application.dataPath + "/Resources/Wallets/" + nameField.text + ".json", nameField.text, account.Address, account.PrivateKey);

        SaveData saveWallet = SaveData.SaveAtPath("Wallets");

        saveWallet.Save<WalletInfo>(newWallet.title, newWallet);


        validationText.text = "Your new wallet has been added! You can add another one or go Back to the wallet list!";

        StartCoroutine(CheckWalletContents(newWallet));

        yield return null;
    }

    /// <summary>
    /// Checks wallet's contents. Returns to debug log.
    /// </summary>
    /// <param name="wallet"></param>
    /// <returns></returns>
    public IEnumerator CheckWalletContents(WalletInfo wallet)
    {
        // Debug.Log(wallet.address);
        var wait = 1;
        bool processDone = false;

        while (!processDone)
        {
            yield return new WaitForSeconds(wait);
            wait = 10;
            EthGetBalanceUnityRequest balanceRequest = new EthGetBalanceUnityRequest("https://mainnet.infura.io");

            yield return balanceRequest.SendRequest(wallet.address, Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest());
            if (balanceRequest.Exception == null)
            {
                Debug.Log(balanceRequest.Result);
               
                processDone = true;
                yield return null;
            }

        }
    }

    /// <summary>
    /// Checks wallet contents, writes result on a given text.
    /// </summary>
    /// <param name="wallet"></param>
    /// <param name="container"></param>
    /// <returns></returns>
    public IEnumerator CheckWalletContents(WalletInfo wallet, Text container)
    {
         Debug.Log(wallet.address);
        var wait = 1;
        bool processDone = false;

        while (!processDone)
        {
            yield return new WaitForSeconds(wait);
            wait = 3;
            EthGetBalanceUnityRequest balanceRequest = new EthGetBalanceUnityRequest("https://mainnet.infura.io");

            yield return balanceRequest.SendRequest(wallet.address, Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest());
            if (balanceRequest.Exception == null)
            {
                container.text = balanceRequest.Result.Value.ToString() ;

                processDone = true;
                yield return null;
            }

        }
    }

    public IEnumerator CheckBlockNumber()
    {
        string password = "hyperbridge";
        string path = "Assets/Hyperbridge/Resources/keystore.json";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        string keystore = reader.ReadToEnd();
        reader.Close();

        var keyStoreService = new Nethereum.KeyStore.KeyStoreService();
        byte[] key = keyStoreService.DecryptKeyStoreFromJson(password, keystore);
        var account = new Account(key);

        Debug.Log(account.Address);
        Debug.Log(account.PrivateKey);

        var wait = 1;
        while (true)
        {
            yield return new WaitForSeconds(wait);
            wait = 10;
            var blockNumberRequest = new EthBlockNumberUnityRequest("https://mainnet.infura.io");
            yield return blockNumberRequest.SendRequest();
            if (blockNumberRequest.Exception == null)
            {
                var blockNumber = blockNumberRequest.Result.Value;
                Debug.Log("Block: " + blockNumber.ToString());
            }
        }
    }

    public void CreateWallet()
    {
        this.StartCoroutine(this.CheckBlockNumber());
    }

    bool KeystoreValidate(string text)
    {

        if (text.Contains("address"))
        {
            validationText.text = "Apparently Valid Keystore File loaded";
            return true;
        }
        else
        {
            validationText.text = "You didn't choose a valid Keystore File";
            return false;
        }
    }

    public void WriteResult(string[] paths)
    {
        if (paths.Length == 0)
        {
            return;
        }

        _path = "";
        foreach (var p in paths)
        {
            _path += p;
        }
    }
}

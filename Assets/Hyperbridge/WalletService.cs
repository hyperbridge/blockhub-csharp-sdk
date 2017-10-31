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


public class Account
{

#if !PCL
    public static Account LoadFromKeyStoreFile(string filePath, string password)
    {
        var keyStoreService = new Nethereum.KeyStore.KeyStoreService();
        var key = keyStoreService.DecryptKeyStoreFromFile(password, filePath);
        return new Account(key);
    }
#endif
    public static Account LoadFromKeyStore(string json, string password)
    {
        var keyStoreService = new Nethereum.KeyStore.KeyStoreService();
        var key = keyStoreService.DecryptKeyStoreFromJson(password, json);
        return new Account(key);
    }

    public Account(EthECKey key)
    {
        Initialise(key);
    }

    public Account(string privateKey)
    {
        Initialise(new EthECKey(privateKey));
    }

    public Account(byte[] privateKey)
    {
        Initialise(new EthECKey(privateKey, true));
    }

    private void Initialise(EthECKey key)
    {
        PrivateKey = key.GetPrivateKey();
        Address = key.GetPublicAddress();
    }

    public string Address { get; protected set; }
    public string PrivateKey { get; protected set; }
}


public class WalletService : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
}


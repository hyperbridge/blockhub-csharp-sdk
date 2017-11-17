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


public class AppliedWalletService : MonoBehaviour
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

    //TextAsset asset = Resources.Load<TextAsset>("keystore");

    //try {
    //    Debug.Log("C");
    //    //Debug.Log(asset.text);
    //    Debug.Log(keystore);
    //    var password = "hyperbridge";
    //    var account = Account.LoadFromKeyStore(keystore, password);
    //    Debug.Log("D");

    //    Nethereum.JsonRpc.Client.IClient client = new Nethereum.JsonRpc.Client.RpcClient(new Uri("https://mainnet.infura.io:443"));
    //    var web3 = new Web3(account, "https://mainnet.infura.io:443"); // , "https://mainnet.infura.io/metamask"
    //    Debug.Log("DD");
    //    var accounts = await web3.Personal.ListAccounts.SendRequestAsync();

    //    Debug.Log("E");
    //    Debug.Log(string.Join(",", accounts));
    //} catch (Exception e) {
    //    Debug.Log(e);
    //}

    public void CreateWallet()
    {
        this.StartCoroutine(this.CheckBlockNumber());
        //await web3.Personal.UnlockAccount.SendRequestAsync("0xbb7e97e5670d7475437943a1b314e661d7a9fa2a", password, new HexBigInteger(60));

        //Debug.Log(web3.Personal.ListAccounts.);
        //"0x" + EthECKey.GetPublicAddress(privateKey); //could do checksum
        //string words = "ripple scissors kick mammal hire column oak again sun offer wealth tomorrow wagon turn fatal";
        //string password = "TREZOR";
        //var wallet = new Wallet(words, password);
        //var account = wallet.GetAccount(0);
    }
}


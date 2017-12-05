using System;
using System.Collections;
using System.IO;
using UnityEngine;
using Nethereum.JsonRpc.UnityClient;
using Nethereum.Hex;
using Nethereum.RPC.Eth.DTOs;
using UnityEngine.UI;
using SFB;
using System.Text.RegularExpressions;
using Hyperbridge.Blockchain.Ethereum;
using Hyperbridge.Core;


namespace Hyperbridge.Wallet
{
    public class WalletService : MonoBehaviour
    {
        public InputField nameField, passwordField;
        public Button tempCreateWallet;
        string _path, keystore;

        float timeoutTimer;
        bool waiting;
        private void Start()
        {
            _path = "";
            timeoutTimer = 0;
            //  tempCreateWallet.onClick.AddListener(() => { InternalWalletSetup("0x50c54C1Cf8b06f0F1Ef5f1DF795d6cb358DE8091", "04975e2906e4a2511babb776a8eecbcb222e844e9030d5004c4599d4b477ce72", "SecondWallet", null, "bitcoin"); });
        }
        private void Update()
        {
            if (waiting)
            {
                if (timeoutTimer >= 10)
                {
                    waiting = false;
                }
            }


        }
        public bool IsWalletInfoComplete(Text validationText)
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

        public void FindKeystore(Text validationText)
        {
            _path = "";

            WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false));
            StreamReader reader = new StreamReader(_path);
            keystore = reader.ReadToEnd();
            reader.Close();
            KeystoreValidate(keystore, validationText);
        }

        public void AcceptWallet(Text validationText)
        {
            if (KeystoreValidate(keystore, validationText) && IsWalletInfoComplete(validationText))
            {
                ConfirmAccount(keystore, validationText, passwordField.text, nameField.text);
            }
        }

        // TODO: Services should not be touching unity components
        public Account ConfirmAccount(string accountKeystore, Text validationText, string password, string walletName)
        {
            // Debug.Log(accountKeystore);
            //Making sure the keystore is in an acceptable format for the account parser.
            string editedJson = Regex.Replace(accountKeystore, @"^""|""$|\n?", ""); //This one may be overkill......
            editedJson = Regex.Unescape(editedJson);  //I think this one is very required, though
                                                      // Debug.Log(editedJson);
            Nethereum.KeyStore.KeyStoreService keyStoreService = new Nethereum.KeyStore.KeyStoreService();
            byte[] key = null;
            try
            {
                key = keyStoreService.DecryptKeyStoreFromJson(password, editedJson);
            }
            catch
            {
                // TODO: ew
                validationText.text = "Password is wrong or keystore is corrupted.";
                return null;

            }

            //        byte[] key = keyStoreService.DecryptKeyStoreFromJson(password, editedJson);

            Account account = new Account(key);
            //Checking no other wallets have the same address, because we'd have a duplicate

            foreach (WalletInfo wallet in AppManager.instance.walletManager.wallets)
            {
                if (wallet.address == account.Address)
                {
                    validationText.text = "This wallet address already exists.";

                }
            }

            return account;
        }

        public IEnumerator SendFundsToAddress(string addressTo, string addressFrom, string gas, string value)
        {
            EthSendTransactionUnityRequest request = new EthSendTransactionUnityRequest("https://mainnet.infura.io");
            var gasForTransaction = new Nethereum.Hex.HexTypes.HexBigInteger(gas);
            var valueForTransaction = new Nethereum.Hex.HexTypes.HexBigInteger(value);
            TransactionInput transaction = new TransactionInput("", addressTo, addressFrom, gasForTransaction, valueForTransaction);

            yield return request.SendRequest(transaction);

        }
        public void InternalWalletSetup(string address, string privateKey, string walletName, Text validationText, string coin)
        {
            var newWallet = new WalletInfo();
            string uuid = Guid.NewGuid().ToString();
            newWallet.Setup(AppManager.instance.walletManager.CurrentWalletPath + "/" + walletName + ".json", walletName, address, privateKey, uuid, coin);

            Database.SaveJSONToExternal<WalletInfo>(AppManager.instance.walletManager.CurrentWalletPath + "/" + uuid, walletName, newWallet);

            validationText.text = "Your new wallet has been added! You can add another one or go Back to the wallet list!";

            StartCoroutine(CheckWalletContents(newWallet));
        }

        public IEnumerator CheckWalletContents(WalletInfo wallet)
        {
            // Debug.Log(wallet.address);
            var wait = 1;
            bool processDone = false;
            int rounds = 0;
            while (!processDone)
            {
                rounds++;
                Debug.Log(rounds);
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

        // We create the function which will check the balance of the address and return a callback with a decimal variable
        public IEnumerator GetAccountBalance(string address, System.Action<decimal> callback)
        {
            // Now we define a new EthGetBalanceUnityRequest and send it the testnet url where we are going to
            // check the address, in this case "https://kovan.infura.io".
            // (we get EthGetBalanceUnityRequest from the Netherum lib imported at the start)
            var getBalanceRequest = new EthGetBalanceUnityRequest("https://mainnet.infura.io");
            // Then we call the method SendRequest() from the getBalanceRequest we created
            // with the address and the newest created block.
            getBalanceRequest.SendRequest(address, Nethereum.RPC.Eth.DTOs.BlockParameter.CreateLatest());

            yield return new WaitUntil(() => getBalanceRequest.Result != null);
            // Now we check if the request has an exception
            if (getBalanceRequest.Exception == null)
            {
                // We define balance and assign the value that the getBalanceRequest gave us.
                var balance = getBalanceRequest.Result.Value;

                // Finally we execute the callback and we use the Netherum.Util.UnitConversion
                // to convert the balance from WEI to ETHER (that has 18 decimal places)
                callback(Nethereum.Util.UnitConversion.Convert.FromWei(balance, 18));

            }
            else
            {
                // If there was an error we just throw an exception.
                throw new System.InvalidOperationException("Get balance request failed");
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

                    //   Debug.Log("Block: " + blockNumber.ToString());
                }
            }
        }

        public void CreateWallet()
        {
            this.StartCoroutine(this.CheckBlockNumber());
        }
        public bool IsWalletLocationValid()
        {
            if (AppManager.instance.walletManager.CurrentWalletPath == Application.persistentDataPath + "/Profiles/" + AppManager.instance.profileManager.activeProfile.uuid + "/Wallets")
            {
                return true;
            }
            else
            {
                if (Directory.Exists(AppManager.instance.walletManager.CurrentWalletPath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        // This function will just execute a callback after it creates and encrypt a new account
        public void CreateAccount(string password, System.Action<string, string, string> callback)
        {
            Debug.Log("[WalletService] Creating account...");
            // We use the Nethereum.Signer to generate a new secret key
            var ecKey = Nethereum.Signer.EthECKey.GenerateKey();

            // After creating the secret key, we can get the public address and the private key with
            // ecKey.GetPublicAddress() and ecKey.GetPrivateKeyAsBytes()
            // (so it return it as bytes to be encrypted)
            var address = ecKey.GetPublicAddress();
            var privateKey = ecKey.GetPrivateKeyAsBytes();

            // Then we define a new KeyStore service
            var keystoreservice = new Nethereum.KeyStore.KeyStoreService();

            // And we can proceed to define encryptedJson with EncryptAndGenerateDefaultKeyStoreAsJson(),
            // and send it the password, the private key and the address to be encrypted.
            var encryptedJson = keystoreservice.EncryptAndGenerateDefaultKeyStoreAsJson(password, privateKey, address);
            // Finally we execute the callback and return our public address and the encrypted json.
            // (you will only be able to decrypt the json with the password used to encrypt it)

            callback(address, privateKey.ToString(), encryptedJson);
        }

        bool KeystoreValidate(string text, Text validationText)
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
}
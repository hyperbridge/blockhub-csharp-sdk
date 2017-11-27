using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.Text.RegularExpressions;
using Hyperbridge.Blockchain.Ethereum;
using Hyperbridge.Wallet;
using Hyperbridge.UI;
using Hyperbridge.Core;

public class CreateWalletView : MonoBehaviour
{
    public Text validationText;
    public InputField walletNameInputField, walletPasswordInputField;
    public Button createWalletButton;
    public GameObject loadingModal;
    public Toggle ethereumToggle, bitcoinToggle;

    void Start() 
    {
        this.createWalletButton.onClick.AddListener(() =>
        {
            this.validationText.text = "Please be patient. Your key is being created.";

            if (this.walletPasswordInputField.text.Length >= 8 && this.walletNameInputField.text.Length != 0)
            {
                this.loadingModal.SetActive(true);

                this.StartCoroutine(this.CreateWallet());
            }
            else if (this.walletPasswordInputField.text.Length < 8)
            {
                this.validationText.text = "Your password MUST BE at least 8 characters long";
            }
            else if (this.walletNameInputField.text.Length == 0)
            {
                this.validationText.text = "Your wallet's name cannot be empty";
            }
        });
    }

    public IEnumerator CreateWallet()
    {
        yield return new WaitForSeconds(0.2f);
        string tempAddress = "", tempJson = "";
        yield return new WaitForThreadedTask(() =>
        {
            AppManager.instance.walletService.CreateAccount(this.walletPasswordInputField.text, (address, encryptedJson) =>
            {
                // We just print the address and the encrypted json we just created
                Debug.Log(address);
                Debug.Log(encryptedJson);
                tempAddress = address;
                tempJson = encryptedJson;
            });
        });

        string newPath = StandaloneFileBrowser.SaveFilePanel("Save Keystore JSON", "", this.walletNameInputField.text, "json");

        SaveData saver = SaveData.SaveAtPath(newPath);
        saver.SaveExternal<string>(this.walletNameInputField.text, tempJson);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif

        Account tempAccount = null;
        yield return new WaitForThreadedTask(() =>
        {
            tempAccount = AppManager.instance.walletService.ConfirmAccount(tempJson, this.validationText, this.walletPasswordInputField.text, this.walletNameInputField.text);
        });
        if (ethereumToggle.isOn)
        {
            AppManager.instance.walletService.InternalWalletSetup(tempAccount, this.walletNameInputField.text, validationText, "Ethereum");

        }
        else if (bitcoinToggle.isOn)
        {
            AppManager.instance.walletService.InternalWalletSetup(tempAccount, this.walletNameInputField.text, validationText, "Bitcoin");

        }
        this.loadingModal.SetActive(false);

        this.gameObject.GetComponent<Devdog.General.UI.UIWindowPage>().Hide();

        CodeControl.Message.Send<NavigateEvent>(new NavigateEvent { path = "/main/wallets" });

        this.StartCoroutine(AppManager.instance.walletManager.LoadWallets());
    }
}

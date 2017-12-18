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
    private Coroutine createWalletRoutine;
    private bool waitingForWallet;
    private float waitingTimer;

    void Start()
    {
        waitingTimer = 0;
        waitingForWallet = false;
        this.createWalletButton.onClick.AddListener(() =>
        {
            this.validationText.text = "Please be patient. Your key is being created.";

            if (this.walletPasswordInputField.text.Length >= 8 && this.walletNameInputField.text.Length != 0)
            {
                this.loadingModal.SetActive(true);

                createWalletRoutine = this.StartCoroutine(this.CreateWallet());
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

    private void Update()
    {
        if (waitingForWallet)
        {
            waitingTimer += Time.deltaTime;
            if (waitingTimer >= 30)
            {
                StopCoroutine(createWalletRoutine);
                WalletCreationTimedOut();
                waitingForWallet = false;
            }
        }
    }

    public IEnumerator CreateWallet()
    {
        yield return new WaitForSeconds(0.2f);

        string tempAddress = "", tempJson = "", key = "";
        if (!AppManager.instance.walletService.IsWalletLocationValid())
        {
            validationText.text = "The Path for wallet saving you have set has issues. Please check settings";
            validationText.color = Color.red;
            NavigateViewOnSuccess();
            yield break;
        }
        waitingTimer = 0;

        waitingForWallet = true;
        yield return new WaitForThreadedTask(() =>
        {
            AppManager.instance.walletService.CreateAccount(this.walletPasswordInputField.text, (address, privateKey, encryptedJson) =>
            {
                key = privateKey;
                tempAddress = address;
                tempJson = encryptedJson;
            });


        });
        Debug.Log(waitingTimer);
        waitingTimer = 0;
        waitingForWallet = false;
        if (ethereumToggle.isOn)
        {
            AppManager.instance.walletService.InternalWalletSetup(tempAddress, key, this.walletNameInputField.text, validationText, "Ethereum");

        }
        else if (bitcoinToggle.isOn)
        {
            AppManager.instance.walletService.InternalWalletSetup(tempAddress, key, this.walletNameInputField.text, validationText, "Bitcoin");

        }

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif

        NavigateViewOnSuccess();
    }

    void NavigateViewOnSuccess()
    {
        this.loadingModal.SetActive(false);

        this.gameObject.GetComponent<Devdog.General.UI.UIWindowPage>().Hide();

        CodeControl.Message.Send<NavigateEvent>(new NavigateEvent { path = "/main/wallets" });

        AppManager.instance.walletManager.RefreshWalletList();
    }

    public void CancelWalletCreation()
    {
        StopCoroutine(this.createWalletRoutine);
        this.loadingModal.SetActive(false);
        validationText.text = "Process has been stopped by user";

    }

    public void WalletCreationTimedOut()
    {
        this.loadingModal.SetActive(false);
        validationText.text = "The process has timed out. Try again later.";

    }
}

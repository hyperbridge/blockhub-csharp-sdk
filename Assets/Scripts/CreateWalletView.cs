using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.Text.RegularExpressions;

public class CreateWalletView : MonoBehaviour
{
    public Text validationText;
    public InputField walletNameInputField, walletPasswordInputField;
    public Button createWalletButton;

    void Start() // TODO: WALLET ANIMATION CREATION. THREADING.
    {
        this.createWalletButton.onClick.AddListener(() => {
            this.validationText.text = "Please be patient. Your key is being created.  ERIC WE NEED A LOADING ANIMATION HERE?";

            if (this.walletPasswordInputField.text.Length >= 8 && this.walletNameInputField.text.Length != 0) {
                AppManager.instance.walletService.CreateAccount(this.walletPasswordInputField.text, (address, encryptedJson) => {
                    // We just print the address and the encrypted json we just created
                    Debug.Log(address);
                    Debug.Log(encryptedJson);

                    string newPath = StandaloneFileBrowser.SaveFilePanel("Save Keystore JSON", "", this.walletNameInputField.text, "json");

                    SaveData saver = SaveData.SaveAtPath(newPath);
                    saver.SaveExternal<string>(this.walletNameInputField.text, encryptedJson);

                    UnityEditor.AssetDatabase.Refresh();

                    this.StartCoroutine(AppManager.instance.walletManager.LoadWallets());

                    this.StartCoroutine(AppManager.instance.walletService.ConfirmAccount(encryptedJson, this.validationText, this.walletPasswordInputField.text, this.walletNameInputField.text));
                });
            }
            else if (this.walletPasswordInputField.text.Length < 8) {
                this.validationText.text = "Your password MUST BE at least 8 characters long";
            }
            else if (this.walletNameInputField.text.Length == 0) {
                this.validationText.text = "Your wallet's name cannot be empty";
            }
        });
    }
}

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





    void Start()
    {
        createWalletButton.onClick.AddListener(() =>
        {
            validationText.text = "Please be patient. Your key is being created.  ERIC WE NEED A LOADING ANIMATION HERE?";

            if (walletPasswordInputField.text.Length >= 8 && walletNameInputField.text.Length !=0)
            {
                AppManager.instance.walletService.CreateAccount(walletPasswordInputField.text, (address, encryptedJson) =>
                {
                    // We just print the address and the encrypted json we just created
                    Debug.Log(address);
                    Debug.Log(encryptedJson);
                    string editedJson = Regex.Replace(encryptedJson, @"^""|""$|\n?", "");

                    string newPath = StandaloneFileBrowser.SaveFilePanel("Save Keystore JSON", "", walletNameInputField.text, "json");
                    SaveData saver = SaveData.SaveAtPath(newPath);
                    
                    saver.SaveExternal<string>(walletNameInputField.text, editedJson);
                    StartCoroutine(AppManager.instance.walletService.ConfirmAccount(editedJson, validationText,walletPasswordInputField.text,walletNameInputField.text));

                });

            }
            else if (walletPasswordInputField.text.Length < 8)
            {
                validationText.text = "Your password MUST BE at least 8 characters long";
            }else if(walletNameInputField.text.Length == 0)
            {
                validationText.text = "Your wallet's name cannot be empty";
            }
            
           
        });

    }


}

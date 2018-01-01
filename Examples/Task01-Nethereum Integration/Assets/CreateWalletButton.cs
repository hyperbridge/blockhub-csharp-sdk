using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Wallet;
using Hyperbridge.Nethereum;

public class CreateWalletButton : MonoBehaviour {
    private Button _Instance;
    private int _Index = 0;

	void Start () {
        _Instance = GetComponent<Button>();
        _Instance.onClick.AddListener(OnButtonClick);
	}
	
    void OnButtonClick()
    {
        // Every click of the button, creates a new account (or shows it based on index).
        IWallet<NethereumAccount> wallet = new NethereumHdWallet("brass bus same payment express already energy direct type have venture afraid");
        IAccount account = wallet.GetAccount(_Index);

        UnityEditor.EditorUtility.DisplayDialog("Account Created.", $"Index: {_Index}, Address: {account.Address}, Private Key: {account.PrivateKey}", "Ok");

        _Index++;
    }
}

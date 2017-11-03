using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletInfoContainer : MonoBehaviour {

    public Text walletName, walletContents;
    public WalletInfo myWallet;
    public Button deleteWalletButton;
    private void Start()
    {
        deleteWalletButton.onClick.AddListener(() =>
        {

            AppManager.instance.walletManager.DeleteWallet(this);


        });
    }
    public void SetupContainer(WalletInfo wallet)
    {
        myWallet = wallet;

        walletName.text = wallet.name;




        StartCoroutine(AppManager.instance.walletService.CheckWalletContents(wallet, walletContents));
    }

    

}

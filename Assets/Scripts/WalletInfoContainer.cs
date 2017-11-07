using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletInfoContainer : MonoBehaviour
{

    public Text title, walletContents;
    public WalletInfo myWallet;
    public Button editWalletButton;
    public Button deleteWalletButton;
    public WalletManagerView walletManagerView;

    private void Start()
    {
        walletManagerView = AppManager.instance.walletManager.walletManagerView;
    }

    public void SetupContainer(WalletInfo wallet)
    {
        myWallet = wallet;

        this.title.text = wallet.title;
        GetComponent<Button>().onClick.AddListener(() => {
            if (walletManagerView == null)
            {
                walletManagerView = AppManager.instance.walletManager.walletManagerView;
            }

            walletManagerView.infoView.SetupView(this);
        });
       /* TODO: Wallet Editing this.editWalletButton.onClick.AddListener(() =>
        {
            if (walletManagerView == null)
            {
                walletManagerView = FindObjectOfType<WalletManagerView>();
            }

            walletManagerView.infoView.SetupView(wallet);

            //walletManagerView.EditWallet(data);
        });*/

        this.deleteWalletButton.onClick.AddListener(() =>
        {
            AppManager.instance.walletManager.DeleteWallet(this);
        });

        // StartCoroutine(AppManager.instance.walletService.CheckWalletContents(wallet, walletContents));
        StartCoroutine(AppManager.instance.walletService.GetAccountBalance(wallet.address, walletContents, (balance) => { }));

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletInfoContainer : MonoBehaviour
{
    public Text title, walletContents;
    public WalletInfo wallet;
    public Button editWalletButton;
    public Button deleteWalletButton;

    public void SetupContainer(WalletInfo wallet)
    {
        this.wallet = wallet;
        this.title.text = wallet.title;

        this.GetComponent<Button>().onClick.AddListener(() =>
        {
            AppManager.instance.walletManager.infoView.SetupView(this.wallet);
        });

        this.deleteWalletButton.onClick.AddListener(() =>
        {
            AppManager.instance.walletManager.DeleteWallet(this.wallet);
        });

        this.StartCoroutine(AppManager.instance.walletService.GetAccountBalance(this.wallet.address, this.walletContents, (balance) => { }));
    }
}

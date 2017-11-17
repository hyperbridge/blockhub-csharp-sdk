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
    public Sprite[] coinTypeSprites;
    Button walletInfoViewButton;
    public Image logo;
    private void Start()
    {
    }
    public void SetupContainer(WalletInfo wallet)
    {
        walletInfoViewButton = GetComponent<Button>();

        this.wallet = wallet;
        this.title.text = wallet.title;
        if (wallet.coinType == "Ethereum")
        {
            logo.sprite = coinTypeSprites[0];
        }else if (wallet.coinType == "Bitcoin")
        {
            logo.sprite = coinTypeSprites[1];

        }
        this.walletInfoViewButton.onClick.AddListener(() =>
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Core;

namespace Hyperbridge.Wallet
{
    public class WalletInfoContainer : MonoBehaviour
    {
        public Text title, walletContents;
        public WalletInfo wallet;
        public Sprite[] coinTypeSprites;
        public Image logo;

        public Button walletInfoViewButton;

        public void SetupContainer(WalletInfo wallet)
        {
            this.wallet = wallet;
            this.title.text = wallet.title;

            if (wallet.coinType == "Ethereum")
            {
                logo.sprite = coinTypeSprites[0];
            }
            else if (wallet.coinType == "Bitcoin")
            {
                logo.sprite = coinTypeSprites[1];

            }

            this.walletInfoViewButton.onClick.AddListener(() =>
            {
                AppManager.instance.walletManager.infoView.SetupView(this.wallet);
            });

            this.StartCoroutine(AppManager.instance.walletService.GetAccountBalance(this.wallet.address, (balance) =>
            {
                this.walletContents.text = balance.ToString();
                Debug.Log(balance);
            }));
        }


    }
}
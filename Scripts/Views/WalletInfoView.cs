using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Devdog.General.UI;
using System;
using Hyperbridge.UI;
using Hyperbridge.Wallet;
using Hyperbridge.Core;
using Hyperbridge.OS;

public class WalletInfoView : MonoBehaviour
{
    public Text title, info, address, secondaryAddress, privateKey;
    public InputField walletNameForEditing, walletNameForCopying;
    public Image walletImage;
    public Button deleteWalletButton;
    public Image QRCodeContainer;

    private QRGenerator qrGen;
    private string currentWalletAddress;

    public void SetupView(WalletInfo wallet)
    {
        this.title.text = wallet.title;
        this.info.text = "More Info: \n" + wallet.info;
        this.currentWalletAddress = wallet.address;
        this.address.text = wallet.address;
        this.secondaryAddress.text = wallet.address;
        this.privateKey.text = wallet.privateKey;
        this.walletNameForEditing.text = wallet.title;
        this.walletNameForCopying.text = this.address.text;

        this.GetComponent<UIWindowPage>().Show();

        this.deleteWalletButton.onClick.AddListener(() =>
        {
            AppManager.instance.walletManager.DeleteWallet(wallet);
            this.GetComponent<UIWindowPage>().Hide();
        });

        StartCoroutine(PlaceQRCode(wallet.address));
    }

    public void CopyAddressToClipboard()
    {
        CopyToSystemClipboard clipper = new CopyToSystemClipboard();
        clipper.CopyStringToSystemClipboard(this.currentWalletAddress);
        clipper = null;
    }

    public IEnumerator PlaceQRCode(string address)
    {
        yield return new WaitForSeconds(1);

        this.qrGen = new QRGenerator();
        Texture2D QRCodeOriginTexture = null;

        yield return QRCodeOriginTexture = this.qrGen.GenerateQR(address);

        Sprite qrCodeSprite = Sprite.Create(QRCodeOriginTexture, new Rect(0, 0, QRCodeOriginTexture.width, QRCodeOriginTexture.height), new Vector2(0.5f, 0.5f));

        this.QRCodeContainer.sprite = qrCodeSprite;

        yield return null;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Devdog.General.UI;

public class WalletInfoView : MonoBehaviour
{
    public Text title, info, address, privateKey;
    public InputField walletNameForEditing, walletNameForCopying;
    public Image walletImage;
    public Button deleteWalletButton;

    public void SetupView(WalletInfo wallet)
    {
        this.title.text = wallet.title;
        this.info.text = "More Info: \n" + wallet.info;
        this.address.text = wallet.address;
        this.privateKey.text = wallet.privateKey;
        this.walletNameForEditing.text = wallet.title;
        this.walletNameForCopying.text = this.address.text;
      
        this.GetComponent<UIWindowPage>().Show();

        this.deleteWalletButton.onClick.AddListener(() => {
            AppManager.instance.walletManager.DeleteWallet(wallet);
            this.GetComponent<UIWindowPage>().Hide();
        });
    }
}

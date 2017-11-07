using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Devdog.General.UI;

public class WalletInfoView : MonoBehaviour
{
    public Text title, info, address, privateKey;
    public Image walletImage;
    public Button deleteWalletButton;
    private void Start()
    {
    }

    public void SetupView(WalletInfoContainer data)
    {
        title.text = data.myWallet.title;
        info.text = "More Info: \n" + data.myWallet.info;
        address.text = data.myWallet.address;
        privateKey.text = data.myWallet.privateKey;

        GetComponent<UIWindowPage>().Show();

        deleteWalletButton.onClick.AddListener(() =>
        {
            AppManager.instance.walletManager.DeleteWallet(data);
            GetComponent<UIWindowPage>().Hide();
        });
    }


}

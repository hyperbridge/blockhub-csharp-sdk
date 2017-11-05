using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WalletInfoView : MonoBehaviour
{
    public Text title, info, address, privateKey;
    public Image walletImage;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetupView(WalletInfo data)
    {
        title.text = data.title;
        info.text = data.info;
        address.text = data.address;
        privateKey.text = data.privateKey;

        gameObject.SetActive(true);
    }
}

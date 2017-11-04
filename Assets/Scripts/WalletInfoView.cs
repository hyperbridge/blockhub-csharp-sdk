using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WalletInfoView : MonoBehaviour
{
    public Text title, info, publicAddress, privateKey;
    public Image walletImage;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetupView(WalletInfo data)
    {
        title.text = data.title;

        gameObject.SetActive(true);
    }
}

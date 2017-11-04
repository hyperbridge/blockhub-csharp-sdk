#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletManagerView : MonoBehaviour
{

    public Image icon;
    public Text title, info;
    public Button deleteButton;
    public WalletInfoView infoView;

    public void DeleteWallet(WalletInfoContainer extension)
    {

    }

}

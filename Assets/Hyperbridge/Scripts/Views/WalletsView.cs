#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hyperbridge.Wallet;

public class WalletsView : MonoBehaviour
{
    public GameObject displayPrefab, listView;
    public WalletInfoView infoView;
    public Text noWalletsText;
    private string noWalletsMessage;

    private void Awake()
    {
        this.CleanWalletDisplay();
        this.noWalletsMessage = "This profile doesn't have any Wallets associated to it. \nPlease create a Wallet.";

        CodeControl.Message.AddListener<UpdateWalletsEvent>(UpdateList);
    }

    public void UpdateList(UpdateWalletsEvent e)
    {
        Debug.Log("Updating Wallet Display List...");

        this.CleanWalletDisplay();

        if (e == null) return;

        this.noWalletsText.text = "";

        var wallets = e.wallets;

        foreach (WalletInfo info in wallets)
        {

            GameObject go = Instantiate(displayPrefab);

            var container = go.GetComponent<WalletInfoContainer>();
            container.SetupContainer(info);

            go.transform.SetParent(this.listView.transform);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.SetActive(true);
        }
    }

    private void CleanWalletDisplay()
    {
        this.noWalletsText.text = this.noWalletsMessage;
        foreach (Transform child in this.listView.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

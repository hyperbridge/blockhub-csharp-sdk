using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WalletManager : MonoBehaviour
{

    public List<WalletInfo> wallets = new List<WalletInfo>();
    public GameObject walletDisplayPrefab;
    public RectTransform walletListDisplay;
    List<Transform> currentWalletDisplays = new List<Transform>();

    public void RefreshWalletList()
    {
        StartCoroutine(ReadWalletList());
    }

    public void FillWalletList()
    {
        foreach (WalletInfo wallet in wallets)
        {
            GameObject newWallet = Instantiate(walletDisplayPrefab, walletListDisplay);
            newWallet.GetComponent<WalletInfoContainer>().SetupContainer(wallet);
            currentWalletDisplays.Add(newWallet.transform);
        }
    }

    public void DeleteWallet(WalletInfoContainer wallet)
    {
        WalletInfo targetWallet = null;

        foreach (WalletInfo walletInfo in wallets)
        {

            if (walletInfo.address == wallet.myWallet.address)
            {
                targetWallet = walletInfo;

            }

        }

        wallets.Remove(targetWallet);
       // Debug.Log(targetWallet._path);
       // Debug.Log(File.Exists(targetWallet._path));
        File.Delete(targetWallet._path);
        Destroy(wallet.gameObject);
    }

    public IEnumerator ReadWalletList()
    {
        walletListDisplay.DetachChildren();
        foreach (Transform t in currentWalletDisplays)
        {
            if(t != null)
            {
                Destroy(t.gameObject);
            }
        }

        currentWalletDisplays.Clear();
        wallets.Clear();

        LoadData loader = LoadData.LoadFromPath("Wallets");
        List<WalletInfo> loadedData = new List<WalletInfo>();
        wallets = loader.LoadAllFromFolder<WalletInfo>();
        yield return loader.LoadAllFromFolder<WalletInfo>();

        FillWalletList();
    }
}

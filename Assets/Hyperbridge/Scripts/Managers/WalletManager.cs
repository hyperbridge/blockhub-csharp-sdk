using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WalletManager : MonoBehaviour
{
    public List<WalletInfo> wallets = new List<WalletInfo>();
    public WalletsView managerView;
    public WalletInfoView infoView;

    private void Awake()
    {
        CodeControl.Message.AddListener<AppInitializedEvent>(this.OnAppInitialized);
    }

    public void OnAppInitialized(AppInitializedEvent e) {
        this.RefreshWalletList();
    }

    public void RefreshWalletList()
    {
        this.StartCoroutine(this.LoadWallets());
    }

    public void DeleteWallet(WalletInfo wallet)
    {
        this.wallets.Remove(wallet);

        File.Delete(wallet._path);

        this.StartCoroutine(this.LoadWallets());
    }

    public IEnumerator LoadWallets()
    {
        LoadData loader = LoadData.LoadFromPath("/Resources/Wallets");
        List<WalletInfo> loadedData = new List<WalletInfo>();

        this.wallets = loader.LoadAllFilesFromSubFolder<WalletInfo>();

        var message = new UpdateWalletsEvent();
        message.wallets = wallets;
        CodeControl.Message.Send<UpdateWalletsEvent>(message);

        yield return wallets;
    }
}

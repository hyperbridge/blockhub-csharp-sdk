using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletInfo {

    public string _path, title, info, address, privateKey, secret;

    // Doesnt seem to be used anywhere
    public void SetupWallet(string path, string title, string address, string privateKey, string secret)
    {
        this._path = path;
        this.title = title;
        this.info = "Test";
        this.address = address;
        this.privateKey = privateKey;
        this.secret = secret;
    }

    public void SetupWallet(string path, string title, string address, string privateKey)
    {
        this._path = path;
        this.title = title;
        this.info = "Test";
        this.address = address;
        this.privateKey = privateKey;
    }
}

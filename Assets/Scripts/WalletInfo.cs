using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletInfo {

    public string _path, title, address, privateKey, secret;
    
    public void SetupWallet(string path, string title, string address, string privateKey, string secret)
    {
        this._path = path;
        this.title = title;
        this.address = address;
        this.privateKey = privateKey;
        this.secret = secret;
    }

    public void SetupWallet(string path, string title, string address, string privateKey)

    {
        this._path = path;
        this.title = title;
        this.address = address;
        this.privateKey = privateKey;
    }
}

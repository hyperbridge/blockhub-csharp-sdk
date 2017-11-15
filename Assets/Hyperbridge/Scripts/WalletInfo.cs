using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletInfo
{
    public string _path, title, info, address, privateKey, secret, uuid;

    // TODO: Remove. Doesnt seem to be used anywhere.
    public void Setup(string path, string title, string address, string privateKey, string secret)
    {
        this.secret = secret;
        this.Setup(path, title, address, privateKey);
    }

    public void Setup(string path, string title, string address, string privateKey)
    {
        this._path = path;
        this.title = title;
        this.info = "Test";
        this.address = address;
        this.privateKey = privateKey;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletInfo {

    public string _path,name, address, publicKey,privateKey,secret;
    
    public void SetupWallet(string path,string walletName, string walletAddress,string walletPublic, string walletPrivate, string walletSecret)
    {
        _path = path;
        name = walletName;
        address = walletAddress;
        publicKey = walletPublic;
        privateKey = walletPrivate;
        secret = walletSecret;
    }

    public void SetupWallet(string path,string walletName, string walletAddress, string walletPrivate)

    {
        _path = path;
        name = walletName;
        address = walletAddress;
        privateKey = walletPrivate;


    }
}

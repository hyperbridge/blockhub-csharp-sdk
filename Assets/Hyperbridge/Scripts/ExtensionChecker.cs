using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;
using UnityEngine;

public class ExtensionChecker
{

    public IEnumerator CheckExternalExtensions(Action<List<ExtensionInfo>> callback)
    {
        List<ExtensionInfo> externalExtensions = new List<ExtensionInfo>();
        LoadData loader = LoadData.LoadFromPath("/Hyperbridge/Resources/TemporaryExtensionSource/");
        externalExtensions =  loader.LoadAllFromFolder<ExtensionInfo>();

        yield return externalExtensions;

        callback(externalExtensions);



    }

    public IEnumerator CheckLocalExtensions(Action<List<ExtensionInfo>> callback)
    {
        List<ExtensionInfo> localExtensions = new List<ExtensionInfo>();
        LoadData loader = LoadData.LoadFromPath("Extensions");
        localExtensions = loader.LoadAllFromFolder<ExtensionInfo>();

        yield return localExtensions;

        callback(localExtensions);

    }



} 


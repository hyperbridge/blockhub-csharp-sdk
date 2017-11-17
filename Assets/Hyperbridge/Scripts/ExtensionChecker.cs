using System.Collections.Generic;
using System.Collections;
using System.IO;
using System;
using UnityEngine;

public class ExtensionChecker
{

    /*  public IEnumerator CheckExternalExtensions(Action<List<ExtensionInfo>> callback)
      {
          List<ExtensionInfo> externalExtensions = new List<ExtensionInfo>();
          LoadData loader = LoadData.LoadFromPath("/Hyperbridge/Resources/TemporaryExtensionSource/");
          externalExtensions =  loader.LoadAllFilesFromSubFolder<ExtensionInfo>();

          yield return externalExtensions;

          callback(externalExtensions);



      }*/

    public IEnumerator CheckExtensions(Action<List<ExtensionInfo>> communityExtensions, Action<List<ExtensionInfo>> localExtensions)
    {
        LoadData loader = LoadData.LoadFromPath("/Resources/Extensions");
        List<ExtensionInfo> commExtensions = new List<ExtensionInfo>();
        List<ExtensionInfo> installedExtensions = new List<ExtensionInfo>();

        if (File.Exists(Application.dataPath + "/Resources/Extensions/community-extensions.json"))
        {
            yield return commExtensions = loader.LoadThisData<List<ExtensionInfo>>("community-extensions");
        }

        if (File.Exists(Application.dataPath + "/Resources/Extensions/extensions.json"))
        {
            yield return installedExtensions = loader.LoadThisData<List<ExtensionInfo>>("extensions");
        }

        List<ExtensionInfo> externalExtensions = loader.LoadAllFilesFromSubFolder<ExtensionInfo>();

        yield return new WaitForSeconds(0.1f);

        foreach (ExtensionInfo extension in externalExtensions)
        {
            if (installedExtensions.Count > 0)
            {
                foreach (ExtensionInfo ext in installedExtensions)
                {
                    if (ext.uuid == extension.uuid)
                    {
                        commExtensions.Remove(extension);
                    }
                    else
                    {
                        for (int i = 0; i < commExtensions.Count; i++)
                        {
                            if (commExtensions[i].uuid == extension.uuid)
                            {

                            }
                            else
                            {
                                commExtensions.Add(extension);
                            }
                        }

                    }

                }
            }
            else if (commExtensions.Count > 0)
            {
                for (int i = 0; i < commExtensions.Count; i++)
                {
                    if (commExtensions[i].uuid == extension.uuid)
                    {

                    }
                    else
                    {
                        commExtensions.Add(extension);
                    }
                }
            }
            else
            {
                commExtensions.Add(extension);
            }
        }

        communityExtensions(commExtensions);
        localExtensions(installedExtensions);
    }

    public IEnumerator CheckLocalExtensions(Action<List<ExtensionInfo>> callback)
    {
        List<ExtensionInfo> localExtensions = new List<ExtensionInfo>();
        LoadData loader = LoadData.LoadFromPath("Extensions");
        localExtensions = loader.LoadAllFilesFromFolder<ExtensionInfo>();

        yield return localExtensions;

        callback(localExtensions);
    }
}


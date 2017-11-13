#pragma warning disable 0168 // variable declared but not used.
#pragma warning disable 0219 // variable assigned but not used.
#pragma warning disable 0414 // private field assigned but not used.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SuperScrollView;

public class WalletManagerView : MonoBehaviour
{

    public WalletInfoView infoView;
    public LoopListView2 loopList;

    private void Awake()
    {
        loopList.InitListView(5, OnGetItemByIndex);
    }

    LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    {
        //Index validation
      //  if (index < 0 || index >= DataSourceMgr.Get.TotalItemCount)
       // {
      //      return null;
      //  }

     // Obtaining item data   ItemData itemData = DataSourceMgr.Get.GetItemDataByIndex(index);
       /* if (itemData == null)
        {
            return null;
        }*/
        //get a new item. Every item can use a different prefab, the parameter of the NewListViewItem is the prefab’name. 
        //And all the prefabs should be listed in ItemPrefabList in LoopListView2 Inspector Setting
        LoopListViewItem2 item = listView.NewListViewItem("WalletContainer");

        //initialize items here TODO
    /*    ListItem2 itemScript = item.GetComponent<ListItem2>();
        if (item.IsInitHandlerCalled == false)
        {
            item.IsInitHandlerCalled = true;
            itemScript.Init();
        }
        itemScript.SetItemData(itemData, index);*/
        return item;
    }
}

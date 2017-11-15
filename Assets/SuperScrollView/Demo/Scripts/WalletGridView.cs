using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SuperScrollView;


public class WalletGridView : MonoBehaviour
{

    //public LoopListView2 loopList;
    int walletCountPerRow, totalWalletCount;
    private void Awake()
    {
        walletCountPerRow = 2;
    }



    void Start()
    {

        totalWalletCount = AppManager.instance.walletManager.wallets.Count;

        //loopList.InitListView(ObjectCount(totalWalletCount, walletCountPerRow), OnGetItemByIndex);


    }

    int ObjectCount(int walletsToGenerate, int walletsPerRow)
    {
        int objects = walletsToGenerate / walletsPerRow;
        if (totalWalletCount % walletCountPerRow > 0)
        {
            objects++;
        }
        return objects;
    }




    //LoopListViewItem2 OnGetItemByIndex(LoopListView2 listView, int index)
    //{
    //    if (index < 0)
    //    {
    //        return null;
    //    }
    //    LoopListViewItem2 item = listView.NewListViewItem("WalletContainerDoubleGridItem");
    //    WalletContainerDoubleGridItemController controller = item.GetComponent<WalletContainerDoubleGridItemController>();
      
    //    for (int i = 0; i < walletCountPerRow; ++i)
    //    {
    //        int itemIndex = index * walletCountPerRow + i;
    //        if (itemIndex >= totalWalletCount)
    //        {
    //            controller.walletInfoContainerList[i].gameObject.SetActive(false);
    //            continue;
    //        }

    //        WalletInfo walletData = AppManager.instance.walletManager.wallets[itemIndex];
    //        if (walletData != null)
    //        {
    //            controller.walletInfoContainerList[i].gameObject.SetActive(true);
    //            controller.walletInfoContainerList[i].SetupContainer(walletData);
    //        }
    //        else
    //        {
    //            controller.walletInfoContainerList[i].gameObject.SetActive(false);
    //        }
    //    }
    //    return item;
    //}
}

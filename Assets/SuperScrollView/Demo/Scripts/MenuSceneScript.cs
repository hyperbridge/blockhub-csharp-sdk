using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SuperScrollView
{
    public class SceneNameInfo
    {
        public string mName;
        public string mSceneName;
        public SceneNameInfo(string name,string sceneName)
        {
            mName = name;
            mSceneName = sceneName;
        }
    }
    class MenuSceneScript: MonoBehaviour
    {
        public Transform mButtonPanelTf;
        SceneNameInfo[] mSceneNameArray = new SceneNameInfo[]
        {
            new SceneNameInfo("Top To Bottom","TopToBottomDemo"),
            new SceneNameInfo("Bottop To Top","BottomToTopDemo"),
            new SceneNameInfo("Left To Right","LeftToRightDemo"),
            new SceneNameInfo("Right To Left","RightToLeftDemo"),
            new SceneNameInfo("Select And Delete","DeleteItemDemo"),
            new SceneNameInfo("Grid View","GridViewDemo"),
            new SceneNameInfo("Chat Message List","ChatMsgListViewDemo"),
            new SceneNameInfo("Change Item Height","ChangeItemHeightDemo"),
            new SceneNameInfo("Pull And Refresh","PullAndRefreshDemo"),
            new SceneNameInfo("Spin Date Picker","SpinDatePickerDemo"),
            new SceneNameInfo("Pull And Load More","PullAndLoadMoreDemo"),
            new SceneNameInfo("Click And Load More","ClickAndLoadMoreDemo"),
            new SceneNameInfo("GridView Delete Item","GridViewDeleteItemDemo"),
            new SceneNameInfo("Responsive GridView","ResponsiveGridViewDemo"),
            new SceneNameInfo("Vertical Gallery Demo","VerticalGalleryDemo"),
            new SceneNameInfo("Horizontal Gallery Demo","HorizontalGalleryDemo"),
            new SceneNameInfo("TreeView Demo","TreeViewDemo"),
            new SceneNameInfo("TreeView\nWith Sticky Head","TreeViewWithStickyHeadDemo"),
        };
        void Start()
        {
            CreateFpsDisplyObj();
            int count = mButtonPanelTf.childCount;
            for(int i = 0;i< count;++i)
            {
                SceneNameInfo info = mSceneNameArray[i];
                Button button = mButtonPanelTf.GetChild(i).GetComponent<Button>();
                button.onClick.AddListener(delegate ()
                {
                    SceneManager.LoadScene(info.mSceneName);
                });
                Text text = button.transform.Find("Text").GetComponent<Text>();
                text.text = info.mName;
            }

        }

        void CreateFpsDisplyObj()
        {
            FPSDisplay fpsObj = FindObjectOfType<FPSDisplay>();
            if(fpsObj != null)
            {
                return;
            }
            GameObject go = new GameObject();
            go.name = "FPSDisplay";
            go.AddComponent<FPSDisplay>();
            DontDestroyOnLoad(go);
        }

    }
}

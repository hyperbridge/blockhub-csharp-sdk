using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchInputController : MonoBehaviour
{

    public GameObject UICover, searchResultContainer;
    public Button clearHistoryButton;
    public VerticalLayoutGroup resultsDisplay;


    void Start()
    {
        UICover.SetActive(false);
    }

    public void ToggleCover()
    {
        UICover.SetActive(!UICover.activeInHierarchy);
    }

    public void OnEndEdit(string searchText)
    {

    }
    public void OnValueChanged(string currentSearchText)
    {

    }
}

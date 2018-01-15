using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButton : MonoBehaviour
{
    private Button Button { get; set; }

    private void Start()
    {
        Button = GetComponent<Button>();
        if (Button == null) throw new System.Exception("Button not found.");
        Button.onClick.AddListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();

    protected void Message(string message)
    {
        UnityEditor.EditorUtility.DisplayDialog($"Information", message, "Ok");
    }
}

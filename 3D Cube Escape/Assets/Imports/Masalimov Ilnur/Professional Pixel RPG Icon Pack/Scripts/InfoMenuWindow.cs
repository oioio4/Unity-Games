using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoMenuWindow : MonoBehaviour
{
    // Icon
    public Sprite Icon;
    // Item Name
    public string Title;
    // Description
    public string Description;


    public void OnClick()
    {
        var menuWindow = Resources.Load<GameObject>("Prefabs/InfoMenuWindow");
        var canvas = FindObjectOfType<Canvas>();
        var newWindow = Instantiate(menuWindow, canvas.transform);

        // Replacing the picture
        Image targetImageComponent = newWindow.transform.Find("Panel").Find("ImageIcon").GetComponentInChildren<Image>();
        targetImageComponent.sprite = Icon;

        // Replacing text
        TMP_Text newWindowTitleMesh = newWindow.transform.Find("Heading1").GetChild(0).GetComponent<TMP_Text>();
        TMP_Text newWindowDescrMesh = newWindow.transform.Find("Heading2").GetChild(0).GetComponent<TMP_Text>();
        newWindowTitleMesh.text = Title;
        newWindowDescrMesh.text = Description;
    }
}

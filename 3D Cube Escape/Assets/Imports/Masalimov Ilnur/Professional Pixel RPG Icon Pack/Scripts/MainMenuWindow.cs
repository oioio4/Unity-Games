using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuWindow : AnimatedWindow
{
    public void OnShowThings()
    {
        var menuWindow = Resources.Load<GameObject>("Prefabs/ThingsMenuWindow");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(menuWindow, canvas.transform);
    }

    public void OnShowEffect()
    {
        var menuWindow = Resources.Load<GameObject>("Prefabs/EffectMenuWindow");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(menuWindow, canvas.transform);
    }

    public void OnShowRunes()
    {
        var menuWindow = Resources.Load<GameObject>("Prefabs/RuneMenuWindow");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(menuWindow, canvas.transform);
    }

    public void OnShowInfo()
    {
        var menuWindow = Resources.Load<GameObject>("Prefabs/InfoMenuWindow");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(menuWindow, canvas.transform);
    }
}

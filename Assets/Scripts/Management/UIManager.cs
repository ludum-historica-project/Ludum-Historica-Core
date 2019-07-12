using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Manager
{
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    public List<UIWindow> topLevelWindows;

    public void AddTopWindow(UIWindow newWindow)
    {
        if (topLevelWindows.Contains(newWindow))
        {
            topLevelWindows.Remove(newWindow);
        }
        if (topLevelWindows.Count > 0) topLevelWindows[topLevelWindows.Count - 1].interactable = false;
        topLevelWindows.Add(newWindow);
        newWindow.interactable = true;
    }

    public void RemoveTopWindow(UIWindow removedWindow)
    {
        if (topLevelWindows.Contains(removedWindow))
        {
            topLevelWindows.Remove(removedWindow);
        }
        if (topLevelWindows.Count > 0) topLevelWindows[topLevelWindows.Count - 1].interactable = true;
    }

    private void Update()
    {
        foreach (var window in topLevelWindows)
        {
            window.UpdateWindow();
        }
    }
}

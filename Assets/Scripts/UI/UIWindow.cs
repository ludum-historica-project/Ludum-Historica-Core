using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class UIWindow : MonoBehaviour
{
    public bool isTopWindow;

    List<Selectable> _selectablesToReEnable = new List<Selectable>();
    private bool _interactable;

    public MonoStateEvent OnWindowOpen;
    public MonoStateEvent OnWindowClose;

    List<UIWindow> childrenWindows = new List<UIWindow>();

    public bool isOpen { get; private set; } = false;

    public bool interactable
    {
        get
        {
            return _interactable;
        }
        set
        {
            if (_interactable != value)
            {
                _interactable = value;
                foreach (Selectable selectable in GetComponentsInChildren<Selectable>())
                {
                    selectable.interactable = _interactable;
                }
            }
        }
    }

    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (isTopWindow && Director.GetManager<UIManager>()) Director.GetManager<UIManager>().AddTopWindow(this);
    }
    public void Open()
    {
        isOpen = true;
        interactable = true;
        _animator.SetBool("Open", true);
        OnWindowOpen.Raise();
        if (isTopWindow && Director.GetManager<UIManager>()) Director.GetManager<UIManager>().AddTopWindow(this);
    }

    public void Close()
    {
        isOpen = false;
        _animator.SetBool("Open", false);
        OnWindowClose.Raise();
        if (isTopWindow && Director.GetManager<UIManager>()) Director.GetManager<UIManager>().RemoveTopWindow(this);
    }

    public void UpdateWindow()
    {
        if (childrenWindows.Count > 0)
        {
            foreach (var childWindow in childrenWindows)
            {
                childWindow.UpdateWindow();
            }
            return;
        }
    }

    public void OpenWindowAsChild(UIWindow window)
    {
        childrenWindows.Add(window);
        window.Open();
        interactable = false;
        window.OnWindowClose.onRaiseEvent.AddListener(() =>
        {
            childrenWindows.Remove(window);
            interactable = true;
        });
    }

}

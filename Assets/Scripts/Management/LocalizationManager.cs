using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : Manager
{
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    public System.Action<LocalizationLanguage> OnLanguageUpdated = (lang) => { };

    public LocalizationLanguage defaultLanguage;
    private LocalizationLanguage _currentLanguage;
    public LocalizationLanguage currentLanguage
    {
        get
        {
            return _currentLanguage;
        }
        private set
        {
            if (_currentLanguage != value)
            {
                _currentLanguage = value;
                OnLanguageUpdated(_currentLanguage);
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (currentLanguage == null) currentLanguage = defaultLanguage;
    }
}

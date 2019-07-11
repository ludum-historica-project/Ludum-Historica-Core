using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LocalizationManager : Manager
{
    const string PLAYERPREFS_KEY_LOCALIZATION = "PLAYERPREFS_KEY_LOCALIZATION";

    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }
    public System.Action<LocalizationLanguage> OnLanguageUpdated = (lang) => { };

    [SerializeField]
    public LocalizationLanguage defaultLanguage;

    [SerializeField]
    private LocalizationLanguage _currentLanguage;
    public LocalizationLanguage CurrentLanguage
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
        if (CurrentLanguage == null) CurrentLanguage = defaultLanguage;
    }

    private IEnumerator Start()
    {
        while (Director.GetManager<PlayerPrefsManager>() == null) { yield return new WaitForEndOfFrame(); }
        if (Director.GetManager<PlayerPrefsManager>().HasKey(PLAYERPREFS_KEY_LOCALIZATION))
        {
            Director.GetManager<PlayerPrefsManager>().LoadObjectAndOverwrite(PLAYERPREFS_KEY_LOCALIZATION, this);
            Director.GetManager<PlayerPrefsManager>().objectsToSaveOnExit[PLAYERPREFS_KEY_LOCALIZATION] = this;
        }
    }

}

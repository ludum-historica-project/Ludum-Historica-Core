using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[CreateAssetMenu(menuName = "Scriptables/Localization/Key")]
[System.Serializable]
public class LocalizationKey : ScriptableObject
{

    [SerializeField]
    public List<LanguageKeyPair> keys = new List<LanguageKeyPair>();
    [System.Serializable]
    public class LanguageKeyPair
    {
        public LocalizationLanguage language;
        public string key;
        public LanguageKeyPair(LocalizationLanguage lang, string text = "")
        {
            language = lang;
            key = text;
        }
    }

    public bool ContainsKey(LocalizationLanguage lang)
    {
        return keys.Any(k => k.language == lang);
    }

    public string this[LocalizationLanguage lang]
    {
        get
        {
            if (lang != null && ContainsKey(lang)) return keys.Where(k => k.language == lang).First().key;
            return lang.languageKeyNotImplementedText;
        }
        set
        {
            if (lang != null)
            {
                if (!keys.Any(k => k.language == lang))
                {
                    keys.Add(new LanguageKeyPair(lang));
                }
                keys.Where(k => k.language == lang).First().key = value;
            }
        }
    }

}

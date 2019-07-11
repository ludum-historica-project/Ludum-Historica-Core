using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]
public class LocalizableText : MonoBehaviour
{
    public LocalizationKey key;
    TextMeshProUGUI _text;

    void Awake()
    {
        if (key == null)
        {
            Destroy(this);
            return;
        }
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        RefreshText(Director.GetManager<LocalizationManager>().CurrentLanguage);
        Director.GetManager<LocalizationManager>().OnLanguageUpdated += RefreshText;
    }

    private void OnDisable()
    {
        if (Director.GetManager<LocalizationManager>())
            Director.GetManager<LocalizationManager>().OnLanguageUpdated -= RefreshText;
    }

    public bool hasParameters;
    string[] _lastParameters;
    string _currentText;

    public void SetParameters(params string[] parameters)
    {
        _lastParameters = parameters;
        _text.text = string.Format(_currentText, parameters);
    }

    void RefreshText(LocalizationLanguage lang)
    {
        _text.text = "";
        if (lang == null) return;
        _currentText = key[lang];
        if (!hasParameters)
        {
            _text.text = _currentText;
        }
        else if (_lastParameters != null)
        {
            _text.text = string.Format(_currentText, _lastParameters);
        }
    }
}

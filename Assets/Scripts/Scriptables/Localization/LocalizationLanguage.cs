using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptables/Localization/Language")]
[System.Serializable]
public class LocalizationLanguage : ScriptableObject
{
    [SerializeField]
    public string languageKeyNotImplementedText = "NOT_IMPLEMENTED";
}

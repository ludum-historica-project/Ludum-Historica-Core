using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LocalizationKey))]
public class LocalizationKeyEditor : Editor
{
    LocalizationKey _tgt;
    private void OnEnable()
    {
        _tgt = (LocalizationKey)target;
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        foreach (var guid in AssetDatabase.FindAssets("t:LocalizationLanguage"))
        {
            var language = AssetDatabase.LoadAssetAtPath<LocalizationLanguage>(AssetDatabase.GUIDToAssetPath(guid));
            if (!_tgt.ContainsKey(language))
            {
                _tgt[language] = language.languageKeyNotImplementedText;
            }
            GUI.color = _tgt[language] == language.languageKeyNotImplementedText ? Color.red : Color.white;
            _tgt[language] = EditorGUILayout.TextField(language.name, _tgt[language]);
        }
        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_tgt);

        }
    }

    private void OnDisable()
    {
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUI.color = Color.red;
        if (GUILayout.Button("Raise", GUILayout.Height(40)))
        {
            (target as GameEvent).Raise();
        }
    }
}

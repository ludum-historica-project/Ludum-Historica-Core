using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Linq;
[CustomEditor(typeof(SceneReference))]
public class SceneReferenceEditor : Editor
{
    public override void OnInspectorGUI()
    {

        int targetBuildIndex = (target as SceneReference).targetSceneBuildIndex;

        SceneAsset currentAsset = null;

        if (targetBuildIndex > -1 && EditorBuildSettings.scenes.Length > targetBuildIndex)
        {
            currentAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(EditorBuildSettings.scenes[targetBuildIndex].path);
        }

        SceneAsset newAsset = (SceneAsset)EditorGUILayout.ObjectField("Scene", currentAsset, typeof(SceneAsset), false);

        if (newAsset != null)
        {
            string newAssetPath = AssetDatabase.GetAssetPath(newAsset);
            var matchingBuildScenes = EditorBuildSettings.scenes.Where(s => s.path == newAssetPath);
            if (matchingBuildScenes.Count() > 0)
            {
                EditorBuildSettingsScene selectedScene = matchingBuildScenes.First();

                for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
                {
                    if (EditorBuildSettings.scenes[i].path == selectedScene.path)
                    {
                        (target as SceneReference).targetSceneBuildIndex = i;
                        EditorUtility.SetDirty(target);
                        break;
                    }
                }

            }
            else
            {
                (target as SceneReference).targetSceneBuildIndex = -1;
            }
        }
        else
        {
            (target as SceneReference).targetSceneBuildIndex = -1;
            EditorGUILayout.HelpBox("No Scene assigned, or last assigned scene not included in build.", MessageType.Warning);
        }

    }
}

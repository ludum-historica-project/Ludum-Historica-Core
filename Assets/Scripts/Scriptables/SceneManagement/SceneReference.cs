using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptables/SceneReference")]
public class SceneReference : ScriptableObject
{
    public int targetSceneBuildIndex;

    public void Load()
    {
        Director.GetManager<SceneTransitionManager>().BeginLoadScene(targetSceneBuildIndex);
    }
}

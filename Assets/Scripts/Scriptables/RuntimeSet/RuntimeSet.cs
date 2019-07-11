using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Scriptables/Runtime Set")]
public class RuntimeSet : ScriptableObject
{
    public List<GameObject> objects = new List<GameObject>();

    public void SubscribeObject(GameObject go)
    {
        if (!objects.Contains(go)) objects.Add(go);
    }

    public void UnSubscribeObject(GameObject go)
    {
        objects.Remove(go);
    }
}

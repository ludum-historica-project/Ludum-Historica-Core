using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : Manager
{
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }
    public void SaveToPlayerPrefs(string key, object data)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
    }
    public T LoadFromPlayerPrefs<T>(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        }
        Debug.LogWarning("Key " + key + " doesn't exist in the PlayerPrefs, returning null.");
        return default(T);
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}

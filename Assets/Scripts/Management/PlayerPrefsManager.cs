using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : Manager
{
    public Dictionary<string, object> objectsToSaveOnExit = new Dictionary<string, object>();

    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }
    public void SaveObject(string key, object data)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(data));
    }
    public T LoadObject<T>(string key, T defaultValue = default(T))
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        }
        Debug.LogWarning("Key " + key + " doesn't exist in the PlayerPrefs, returning null.");
        return defaultValue;
    }

    public void LoadObjectAndOverwrite<T>(string key, T obj)
    {
        if (PlayerPrefs.HasKey(key))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(key), obj);
            return;
        }
        Debug.LogWarning("Key " + key + " doesn't exist in the PlayerPrefs.");
    }

    public bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(key);
    }

    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OnDisable()
    {
        foreach (var pair in objectsToSaveOnExit)
        {
            SaveObject(pair.Key, pair.Value);
        }
    }
}

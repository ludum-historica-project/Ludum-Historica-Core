using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : Manager
{
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    public List<ScriptableObject> saveableObjects = new List<ScriptableObject>();

    public void SaveData(int slot = 0)
    {
        Dictionary<string, string> jsonDictionary = new Dictionary<string, string>();
        foreach (var saveableObject in saveableObjects)
        {
            jsonDictionary[saveableObject.name] = JsonUtility.ToJson(saveableObject);
        }

        BinaryFormatter bf = new BinaryFormatter();
        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }
        FileStream file = File.Create(Application.persistentDataPath + "/saves/save" + slot + ".save");
        bf.Serialize(file, jsonDictionary);
        file.Close();
    }

    public void LoadData(int slot = 0)
    {
        if (File.Exists(Application.persistentDataPath + "/saves/save" + slot + ".save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saves/save" + slot + ".save", FileMode.Open);
            Dictionary<string, string> data = (Dictionary<string, string>)bf.Deserialize(file);
            file.Close();

            foreach (var saveableObject in saveableObjects)
            {
                if (data.ContainsKey(saveableObject.name))
                {
                    JsonUtility.FromJsonOverwrite(data[saveableObject.name], saveableObject);
                }
                else
                {
                    Debug.LogWarning(string.Format("[SaveManager] No data for {0} found in save slot {1}.", saveableObject.name, slot));
                }
            }
        }
        else
        {
            Debug.LogWarning("No save file found at slot " + slot);
        }
    }
}

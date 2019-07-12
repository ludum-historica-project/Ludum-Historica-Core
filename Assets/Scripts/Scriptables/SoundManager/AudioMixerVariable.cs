using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[CreateAssetMenu(menuName = "Scriptables/Sound/AudioMixerVariable")]
public class AudioMixerVariable : ScriptableObject

{
    public AudioMixer audioMixer;
    public string mixerProperty;
    [HideInInspector]
    public float lastValue;

    public bool convertToDecibels = false;

    public void SetMixerGroupProperty(float value)
    {
        lastValue = value;
        audioMixer.SetFloat(mixerProperty, convertToDecibels ? LinearToDB(value) : value);
    }

    float LinearToDB(float linear)
    {
        linear = Mathf.Clamp(linear, 0.001f, 1);
        return Mathf.Log(linear, 5) * 20;
    }

}

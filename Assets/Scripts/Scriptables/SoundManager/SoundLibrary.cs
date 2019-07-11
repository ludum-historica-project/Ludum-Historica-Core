using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptables/Sound/Sound Library")]
public class SoundLibrary : ScriptableObject
{
    public List<AudioClip> clips = new List<AudioClip>();
    [Range(0, 1)]
    public float volume = 1;
    [Range(-3, 3)]
    public float pitch = 1;

    public AudioMixerGroup mixerGroup;
    public bool loop;
    public AudioClip GetClip()
    {
        if (clips.Count > 0)
        {
            return clips[Random.Range(0, clips.Count)];
        }
        return null;
    }
}

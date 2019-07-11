using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Manager
{
    protected override void SubscribeToDirector()
    {
        Director.SubscribeManager(this);
    }

    public AudioMixerGroup defaultAudioMixerGroup;

    public Dictionary<SoundLibrary, List<AudioSource>> activeSources = new Dictionary<SoundLibrary, List<AudioSource>>();

    List<AudioSource> _availableSources = new List<AudioSource>();

    public void PlaySound(SoundLibrary soundLib, bool allowMultiple = true)
    {
        bool addSource = false;
        if (activeSources.ContainsKey(soundLib))
        {
            if (activeSources[soundLib].Count > 0 && allowMultiple)
            {
                addSource = true;
            }
        }
        else
        {
            activeSources[soundLib] = new List<AudioSource>();
            addSource = true;
        }
        if (addSource)
        {
            var source = GetNewSource(soundLib);
            activeSources[soundLib].Add(source);
            source.Play();
            source.gameObject.name = string.Format("[Playing sound: {0}]", soundLib.name);
        }

    }

    AudioSource GetNewSource(SoundLibrary soundLib)
    {
        AudioSource source;
        if (_availableSources.Count > 0)
        {
            source = _availableSources[0];
            _availableSources.RemoveAt(0);
        }
        else
        {
            source = new GameObject("[Available AudioSource]").AddComponent<AudioSource>();
            source.transform.SetParent(transform);
        }
        AssignSoundToSource(soundLib, source);
        return source;
    }

    void AssignSoundToSource(SoundLibrary soundLib, AudioSource source)
    {
        source.clip = soundLib.GetClip();
        source.volume = soundLib.volume;
        source.pitch = soundLib.pitch;
        source.loop = soundLib.loop;
        source.outputAudioMixerGroup = soundLib.mixerGroup ? soundLib.mixerGroup : defaultAudioMixerGroup;
    }

    public void StopSound(SoundLibrary soundLib)
    {
        if (activeSources.ContainsKey(soundLib))
        {
            foreach (AudioSource source in activeSources[soundLib])
            {
                source.Stop();
                _availableSources.Add(source);
                source.gameObject.name = "[Available AudioSource]";
            }
            activeSources[soundLib].Clear();
        }
    }

    public void StopAllSounds()
    {
        foreach (var kvp in activeSources)
        {
            foreach (AudioSource source in kvp.Value)
            {
                source.Stop();
                _availableSources.Add(source);
                source.gameObject.name = "[Available AudioSource]";
            }
            kvp.Value.Clear();
        }
        activeSources.Clear();
    }

    private void Update()
    {
        foreach (var kvp in activeSources)
        {
            for (int i = kvp.Value.Count - 1; i >= 0; i--)
            {
                var source = kvp.Value[i];
                if (!source.isPlaying)
                {
                    kvp.Value.RemoveAt(i);
                    _availableSources.Add(source);
                    source.gameObject.name = "[Available AudioSource]";
                }
            }
        }
    }
}

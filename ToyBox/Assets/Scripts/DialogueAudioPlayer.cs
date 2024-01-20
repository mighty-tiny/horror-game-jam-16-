using System.Collections.Generic;
using UnityEngine;

public class DialogueAudioPlayer
{
    private GameObject _sourcePrefab;
    private AudioClip _clip;

    private List<AudioSource> sources;

    public DialogueAudioPlayer(GameObject sourcePrefab, float timePeriod) : this(sourcePrefab, sourcePrefab.GetComponent<AudioSource>().clip) {}

    public DialogueAudioPlayer(GameObject sourcePrefab, AudioClip clip)
    {
        _sourcePrefab = sourcePrefab;
        _clip = clip;

        sources = new();
        GameObject currentObject;
        AudioSource currentSource;

        for(int i = 0; i < 20; i++)
        {
            currentObject = (GameObject)Object.Instantiate(sourcePrefab);
            
            currentSource = currentObject.GetComponent<AudioSource>();
            currentSource.clip = clip;

            sources.Add(currentSource);
        }
    }

    public void Play()
    {
        int i = -1;

        while(++i < sources.Count && sources[i].isPlaying) {}

        (i == sources.Count ? sources[0] : sources[i]).Play();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioUtility
{
    public static AudioService AudioService { private get; set; }

    public static void PlayAudioCue(AudioClip clip)
    {
        AudioService.PlayAudioCue(clip);
    }

    public static void PlayMusic(AudioClip clip)
    {
        AudioService.PlayMusic(clip);
    }
}

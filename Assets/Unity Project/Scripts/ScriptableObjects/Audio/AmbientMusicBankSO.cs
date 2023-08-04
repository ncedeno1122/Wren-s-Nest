using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AmbientMusicBank", menuName = "AmbientMusicBank")]
public class AmbientMusicBankSO : ScriptableObject
{
    // Timed Tracks
    public AudioClip[] DaytimeTracks;
    public AudioClip[] NighttimeTracks;
    
    // Seasonal Tracks
    public AudioClip[] SpringAmbientTracks;
    public AudioClip[] SummerAmbientTracks;
    public AudioClip[] AutumnAmbientTracks;
    public AudioClip[] WinterAmbientTracks;
    
    // + + + + | Functions | + + + + 

    public AudioClip GetRandomTrackFrom(AudioClip[] trackArr)
    {
        return trackArr[Random.Range(0, trackArr.Length)];
    }
}

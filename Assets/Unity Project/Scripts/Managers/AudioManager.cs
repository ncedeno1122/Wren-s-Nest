using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class AudioManager : MonoBehaviour
{
    public const float FADE_TIME = 4.5f;
    public const float VOLUME_MAX = 0.7f;
    public bool IsAMusicTrackPlaying { get; private set; }
    public AudioClip CurrentMusicClip { get; private set; }

    public AudioSource AmbientAudioSrc, MusicAudioSrc;
    public AudioMixer MasterMixer;

    public UnityEvent<AudioClip> OnPlayAudio = new();
    public UnityEvent<AudioClip> OnSwapAudioTrack = new();
    public UnityEvent<AudioClip> OnFinishedAudioTrack = new();

    public AnimationCurve AudioFadeInCurve;

    private IEnumerator m_SongEventTimerCRT;
    private void Awake()
    {
        // Get AudioSources
        List<AudioSource> audioSourceChildren = GetComponentsInChildren<AudioSource>().ToList();
        foreach (AudioSource src in audioSourceChildren)
        {
            // TODO: Don't like this...
            switch (src.outputAudioMixerGroup.name)
            {
                case "AmbientWorldMixer":
                    AmbientAudioSrc = src;
                    break;
                case "MusicMixer":
                    MusicAudioSrc = src;
                    break;
                default:
                    Debug.LogWarning($"AudioSource {src.name} has output group {src.outputAudioMixerGroup.name} that isn't recognized!");
                    break;
            }
        }
        
    }

    void Start()
    {
        
    }
    
    // + + + + | Functions | + + + + 

    public void PlayMusicTrack(AudioClip trackClip)
    {
        StartCoroutine(FadeInNewTrackCRT(trackClip));
    }

    private IEnumerator FadeAudioCRT(float targetVolume, float timeSeconds)
    {
        //Debug.Log("STARTING Fading Audio!");
        float initVolume = MusicAudioSrc.volume;
        for (float i = 0f; i <= timeSeconds; i += Time.deltaTime)
        {
            //MusicAudioSrc.volume = Mathf.SmoothStep(initVolume, targetVolume,  i / timeSeconds);
            MusicAudioSrc.volume = AudioFadeInCurve.Evaluate(i / timeSeconds) * VOLUME_MAX;
            yield return new WaitForEndOfFrame();
        }
        //Debug.Log("FINISHED Fading Audio!");
    }

    private IEnumerator FadeInNewTrackCRT(AudioClip newTrack)
    {
        // Trigger Swap Event
        OnSwapAudioTrack?.Invoke(newTrack);
        
        // Are we playing a track?
        if (MusicAudioSrc.isPlaying || (CurrentMusicClip != null && MusicAudioSrc.clip != null))
        {
            // First, fade out the current track,
            yield return FadeAudioCRT(0f, FADE_TIME);
            MusicAudioSrc.Stop();
        }

        // Then, change the clip
        MusicAudioSrc.clip = newTrack;
        CurrentMusicClip = newTrack;
        
        // Start the timer, then play!
        if (m_SongEventTimerCRT != null)
        {
            StopCoroutine(m_SongEventTimerCRT);
            m_SongEventTimerCRT = null;
        }
        m_SongEventTimerCRT = AudioTrackEventTimerCRT(newTrack);
        StartCoroutine(m_SongEventTimerCRT);
        //MusicAudioSrc.Play();

        // Then, fade into the new track
        //yield return FadeAudioCRT(VOLUME_MAX, FADE_TIME); // TODO: Make some scalable volume float as an option!
    }

    private IEnumerator AudioTrackEventTimerCRT(AudioClip track)
    {
        // Trigger Play Event
        IsAMusicTrackPlaying = true;
        OnPlayAudio?.Invoke(track);
        
        MusicAudioSrc.Play();
        
        // Fade in song
        yield return FadeAudioCRT(VOLUME_MAX, FADE_TIME);

        // Wait duration of the song to trigger fadeout?
        yield return new WaitForSeconds(track.length - (FADE_TIME * 2f));
        
        // Then, fade out
        yield return FadeAudioCRT(0f, FADE_TIME);

        MusicAudioSrc.Stop();
        
        // Trigger Exit Event
        IsAMusicTrackPlaying = false;
        OnFinishedAudioTrack?.Invoke(track);
    }
}

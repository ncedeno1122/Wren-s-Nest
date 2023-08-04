using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioManager))]
public class AmbientAudioPlayerScript : MonoBehaviour
{
    public const float SONG_TIMEBETWEEN = 5f; // 15?

    private AudioManager m_AudioManager;
    public AmbientMusicBankSO AmbientMusicBank;

    [SerializeField] private List<AudioClip> m_LastFiveTracks = new();

    public WaitForSeconds SongWaitTimeCRT = new WaitForSeconds(SONG_TIMEBETWEEN);

    private void Awake()
    {
        m_AudioManager = GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        m_AudioManager.OnPlayAudio.AddListener(HandleAudioPlayed);
        m_AudioManager.OnSwapAudioTrack.AddListener(HandleAudioSwitched);
        m_AudioManager.OnFinishedAudioTrack.AddListener(HandleAudioFinished);
    }
    
    private void OnDisable()
    {
        m_AudioManager.OnPlayAudio.RemoveListener(HandleAudioPlayed);
        m_AudioManager.OnSwapAudioTrack.RemoveListener(HandleAudioSwitched);
        m_AudioManager.OnFinishedAudioTrack.RemoveListener(HandleAudioFinished);
    }

    private void Start()
    {
        StartCoroutine(WaitThenPlayRandomTrack());
    }

    // + + + + | Functions | + + + + 

    private void HandleAudioPlayed(AudioClip newTrack)
    {
        //Debug.Log($"Playing {newTrack.name}!");
    }

    private void HandleAudioSwitched(AudioClip newTrack)
    {
        // 
    }

    private void HandleAudioFinished(AudioClip finishedTrack)
    {
        StartCoroutine(WaitThenPlayRandomTrack());
    }

    private IEnumerator WaitThenPlayRandomTrack()
    {
        yield return SongWaitTimeCRT;
        
        // Then, play random track!
        m_AudioManager.PlayMusicTrack(GetAmbientTrack());
        yield return null;
    }

    private AudioClip GetAmbientTrack()
    {
        // Get CurrentSeason
        Season currSeason = TimeManager.Instance.GetCurrentSeason();
        
        // Get random preference number
        float randomPreference = Random.value;

        if (randomPreference > 0.5f)
        {
            // Play Seasonal Tracks
            switch (currSeason)
            {
                // Get SeasonalSongs
                case Season.SPRING:
                    return AmbientMusicBank.GetRandomTrackFrom(AmbientMusicBank.SpringAmbientTracks);
                case Season.SUMMER:
                    return AmbientMusicBank.GetRandomTrackFrom(AmbientMusicBank.SummerAmbientTracks);
                case Season.AUTUMN:
                    return AmbientMusicBank.GetRandomTrackFrom(AmbientMusicBank.AutumnAmbientTracks);
                case Season.WINTER:
                    return AmbientMusicBank.GetRandomTrackFrom(AmbientMusicBank.WinterAmbientTracks);
                default:
                    Debug.LogWarning($"Season {currSeason} was invalid? Playing a random track");
                    return AmbientMusicBank.GetRandomTrackFrom(AmbientMusicBank.SpringAmbientTracks);
            }
        }
        else
        {
            // Otherwise, return a Time-dependent Track
            if (TimeManager.Instance.CurrentDT.Hour is >= 6 and < 18)
            {
                return AmbientMusicBank.GetRandomTrackFrom(AmbientMusicBank.DaytimeTracks);
            }
            else
            {
                return AmbientMusicBank.GetRandomTrackFrom(AmbientMusicBank.NighttimeTracks);
            }
        }
    }
}

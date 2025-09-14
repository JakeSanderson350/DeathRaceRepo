using EditorAttributes;
using ImprovedTimers;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioMixerGroup musicMixer;
    [SerializeField] AudioClip defaultMusicClip;

    MusicPlaylist playlist;

    AudioSource activeTrack;
    AudioSource fadeTrack;
    CountdownTimer playlistTimer;

    private void OnEnable()
    {
        EventManager.onSceneLoaded += PlayOnAwake;
    }
    private void OnDisable()
    {
        EventManager.onSceneLoaded -= PlayOnAwake;
    }

    void PlayOnAwake(LevelData levelData)
    {
        if (activeTrack != null)
            StopMusic();

        if(levelData.playOnSceneLoad && levelData.playlist != null)
            PlayPlaylist(levelData.playlist);
    }

    public void PlayPlaylist(MusicPlaylist playlist)
    {
        this.playlist = playlist;
        PlayMusic(playlist.CurrentTrack, playlist.ignoreLoopedTracks ? false : playlist.CurrentTrack.loop);
    }

    [Button] void DebugNextTrack() => TrackNext();
    public void TrackNext(bool loop = false) => PlayMusic(playlist.NextTrack(), loop);
    public void TrackSet(int trackNum, bool loop = false) => PlayMusic(playlist.SetTrack(trackNum), loop);

    void PlayMusic(MusicTrack music, bool loop = false)
    {
        if (activeTrack != null)
            StopMusic();

        activeTrack = AudioPlayer.PlaySFX(music.track, transform);
        activeTrack.spatialBlend = 0;
        activeTrack.outputAudioMixerGroup = musicMixer;
        activeTrack.Play();

        if(loop) return;

        playlistTimer = new(Mathf.Clamp(music.Duration - music.fadeTime, 0f, music.Duration));
        playlistTimer.OnTimerStop += TrackTimerExpired;
        playlistTimer.Start();
    }

    void TrackTimerExpired()
    {
        StopMusic(playlist.CurrentTrack.fadeTime);
        PlayMusic(playlist.NextTrack());
    }

    public void StopMusic(float duration = 1f)
    {
        if(fadeTrack != null) 
            KillTrack(fadeTrack);

        if (activeTrack == null)
            return;

        playlistTimer?.Dispose();
        fadeTrack = activeTrack;
        activeTrack = null;
        StartCoroutine(FadeTrack(fadeTrack, duration));
    }

    IEnumerator FadeTrack(AudioSource track, float duration)
    {
        float tick = 0.02f;
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(tick);
        float countdown = duration;

        while (countdown > 0f && track != null)
        {
            countdown -= tick;
            track.volume = Mathf.InverseLerp(0f, duration, countdown);
            yield return delay;
        }

        if (fadeTrack != null)
            KillTrack(fadeTrack);

        fadeTrack = null;
    }

    void KillTrack(AudioSource track)
    {
        track.Stop();
        Destroy(track.gameObject);
    }


}

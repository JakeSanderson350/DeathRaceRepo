using UnityEngine;

[CreateAssetMenu(fileName = "new track", menuName = "Music/Track")]
public class MusicTrack : ScriptableObject
{
    public string displayName;
    public string artist;
    public float fadeTime = 3f;
    public bool loop;
    public AudioClip track;

    //full loop system, quite ambitious, very possible
    //public AudioClip trackBody;
    //public AudioClip trackIntro;
    //public AudioClip trackOutro;

    public float Duration => track.length;
}

using EditorAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Playlist", menuName = "Music/Playlist")]
public class MusicPlaylist : ScriptableObject
{
    //READ ME
    //https://youtu.be/ej7QVKfIuKg?t=172

    int index = 0;

    public bool crossfade;
    public bool ignoreLoopedTracks;
    public List<MusicTrack> playlist;


    public MusicTrack CurrentTrack => index >= playlist.Count ? NextTrack() : playlist[index];

    public MusicTrack NextTrack()
    {
        index = index >= playlist.Count ? 0 : index;
        return playlist[index++]; //Huzzah! A use case!
    }

    public MusicTrack SetTrack(int i)
    {
        index = i >= playlist.Count ? 0 : i;
        return playlist[index++];
    }
}

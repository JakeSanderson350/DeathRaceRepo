using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlaylistControler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI artist;

    private void OnEnable()
    {
        MusicManager.onTrackStarted += SetTrack;
    }

    private void OnDisable()
    {
        MusicManager.onTrackStarted -= SetTrack;
    }

    public void SetTrack(MusicTrack track)
    {
        title.text = track.name;
        artist.text = track.artist;
    }

    public void NextTrack()
    {
        GameManager.inst.GetComponent<MusicManager>().TrackNext();
    }
}

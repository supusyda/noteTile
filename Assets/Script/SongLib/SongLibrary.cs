using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SongLibrary", menuName = "SongLibrary")]

public class SongLibrary : ScriptableObject
{
    // Start is called before the first frame update
    public List<SongSO> songs = new List<SongSO>();
    public SongSO? GetAudioClipsFromName(string name)
    {
        foreach (SongSO song in songs)
        {
            if (song.SongName == name)
            {
                return song;
            }
        }
        return null;
    }
}



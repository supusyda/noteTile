using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataTransferScene", menuName = "DataTransferScene")]
public class DataTransferSence : ScriptableObject
{
    [SerializeField] public SongSO SelectedSong;
    [SerializeField] private SongSO defaultSong;
    public void SaveTransferData(SongSO songSO = null)
    {
        if (songSO == null)
        {
            SelectedSong = defaultSong;
            return;
        }
        SelectedSong = songSO;
    }
    public SongSO GetSelectedSong()
    {
        return SelectedSong != null ? SelectedSong : defaultSong;
    }
    public void ClearData()
    {
        SelectedSong = null;
    }
    public void ClearSOData()
    {

        SelectedSong = null;

    }

}

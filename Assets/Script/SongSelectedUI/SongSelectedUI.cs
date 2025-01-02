using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSelectedUI : MonoBehaviour
{
    [SerializeField] SongLibrary songLibrary;
    [SerializeField] DataTransferSence dataTransferSence;
    [SerializeField] Transform SongSelectedPrefab;
    [SerializeField] Transform contentHolder;
    public static SongSelectedUI instance;
    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }
    void Start()
    {
        dataTransferSence.ClearData();
        InitSongSelectedBtn();
    }


    private void InitSongSelectedBtn()
    {
        foreach (var song in songLibrary.songs)
        {
            Transform songSelected = Instantiate(SongSelectedPrefab, contentHolder);
            songSelected.GetComponent<SongPicker>().SetSong(song);
            songSelected.parent = contentHolder;
            songSelected.gameObject.SetActive(true);

        }
    }
    public void SetSelectedSong(SongSO song)
    {
        dataTransferSence.SaveTransferData(song);
    }
}

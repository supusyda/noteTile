using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongPicker : BtnBase
{
    [SerializeField] private TMP_Text songName;
    [SerializeField] private Image songPic;

    private SongSO song;
    public void SetSong(SongSO song)
    {
        this.song = song;
        songName.text = song.SongName;
        if (song.SongPic == null) return;
        songPic.sprite = song.SongPic;
    }
    override protected void OnClick()
    {
        SongSelectedUI.instance.SetSelectedSong(song);
        TransistionScene.Instance.TransitionToScene(TransistionScene.Instance.GAME_SCENE, "Circle");
    }
}

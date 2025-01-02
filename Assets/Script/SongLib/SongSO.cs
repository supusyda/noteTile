using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Song", menuName = "SongSO")]

public class SongSO : ScriptableObject
{
    // Start is called before the first frame update

    [SerializeField] public TextAsset IntervalTextFile;
    [SerializeField] public AudioClip SongAudio;
    [SerializeField] public string SongName;
    [SerializeField] public Sprite SongPic;

}

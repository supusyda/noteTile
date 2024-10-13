using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _lengthOfCurrentSpawnNote;
    [SerializeField] private float _nextNoteTimer = 0;
    [SerializeField] private TextAsset textFile;
    [SerializeField] private List<float> timeIntervalNote = new List<float>();
    private float _pixelToUnitRatio = 0.01f;
    private float _normalNoteLengthMax = 0.24f;

    private int _currentIndexIntervalNote = 0;
    public bool FirstNotePlayed = false;
    public static NoteManager Instance;
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }


    void Start()
    {
        FirstNotePlayed = false;

        textFile = Resources.Load("Text/interval") as TextAsset; // load data.txt
        string[] data = textFile.text.Split(","); // get data
        foreach (string item in data)
        {
            timeIntervalNote.Add(float.Parse(item, System.Globalization.CultureInfo.InvariantCulture));//change string to float then add to timeIntervalNote
        }
        SpawnNoteArcordingToTextFile();
    }
    private void SpawnNoteArcordingToTextFile()
    {
        float offSetY = 0;//first note position
        _lengthOfCurrentSpawnNote = timeIntervalNote[_currentIndexIntervalNote];//get first note length
        Transform firstNote = SpawnNoteWithOffSet(offSetY);//spawn first note at position 0
        ScaleObjectToDistance(_lengthOfCurrentSpawnNote, firstNote);//scale y = note length
        timeIntervalNote.ForEach((eachNoteDistance) =>
        {
            offSetY += _lengthOfCurrentSpawnNote;//update spawn position(offSetY) = offSetY + previous note length
            SetToNextNoteLength();// set _lengthOfCurrentSpawnNote to next note length
            Transform note = SpawnNoteWithOffSet(offSetY);//spawn note at update offSetY position
            ScaleObjectToDistance(_lengthOfCurrentSpawnNote, note);//scale y = note length

        });
    }
    private Transform SpawnNoteWithOffSet(float oY)
    {
        float unityUnit = 10;
        float realOffSetY = oY * unityUnit;
        // check if note length > 0.24f then spawn note is Hold Note else Normal note
        Transform randNote = NoteSpanwer.Instance.SpawnRandomOfSetY(realOffSetY, IsHoldNote(_lengthOfCurrentSpawnNote));
        randNote.gameObject.SetActive(true);
        return randNote;
    }
    private bool IsHoldNote(float noteLength)
    {
        return noteLength > _normalNoteLengthMax;
    }

    void ScaleObjectToDistance(float distant, Transform note)
    {
        // Calculate the distance between the two objects in Unity units
        // basiclly realOffset
        float yDistance = distant * 10;

        // Convert 550 pixels to Unity units
        float targetLengthInUnits = 550 * _pixelToUnitRatio;




        // Calculate the scale factor needed
        float noteLengthScaleFactor = yDistance / targetLengthInUnits;

        note.GetComponent<Note>().SetLength(noteLengthScaleFactor);

    }
    private void SetToNextNoteLength()
    {
        if (_currentIndexIntervalNote >= timeIntervalNote.Count - 1) return;
        _lengthOfCurrentSpawnNote = timeIntervalNote[++_currentIndexIntervalNote];
    }
    // private float GetNoteLength()
    // {
    //     return _currentIndexIntervalNote + 1;
    // }


}

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _lengthOfCurrentSpawnNote;
    public static NoteManager Instance;
    public DataTransferSence DataTransferSenceData;
    public bool FirstNotePlayed = false;
    private List<float> _timeIntervalNote = new List<float>();
    private int _currentIndexIntervalNote = 0;
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
    }
    void OnEnable()
    {
        EventDefine.onSuccessClickOnNote?.AddListener(CheckFirstNoteClick);
    }
    void OnDisable()
    {
        EventDefine.onSuccessClickOnNote?.RemoveListener(CheckFirstNoteClick);
    }
    private void CheckFirstNoteClick()
    {
        if (NoteManager.Instance.FirstNotePlayed == true) return;
        EventDefine.onStart?.Invoke();
        NoteManager.Instance.FirstNotePlayed = true;
    }

    void Start()
    {
        FirstNotePlayed = false;

        GetDataFormFile();
        SpawnNoteArcordingToTextFile();
    }
    private void SpawnNoteArcordingToTextFile()
    {
        float offSetY = 0;//first note position
        int currentIndexIntervalNote = 0;
        float lengthOfCurrentSpawnNote = _timeIntervalNote[currentIndexIntervalNote];//get first note length

        Note firstNote = SpawnNoteWithOffSet(offSetY, Note.IsHoldNote(lengthOfCurrentSpawnNote)).GetComponent<Note>();//spawn first note at position 0

        firstNote.ScaleToDataLenght(lengthOfCurrentSpawnNote);//scale y = note length


        for (currentIndexIntervalNote = 1; currentIndexIntervalNote < _timeIntervalNote.Count; currentIndexIntervalNote++)
        {
            offSetY += lengthOfCurrentSpawnNote;//update spawn position(offSetY) = offSetY + previous note length


            lengthOfCurrentSpawnNote = _timeIntervalNote[currentIndexIntervalNote];//get next note length

            Note note = SpawnNoteWithOffSet(offSetY, Note.IsHoldNote(lengthOfCurrentSpawnNote)).GetComponent<Note>();//spawn note at update offSetY position

            note.ScaleToDataLenght(lengthOfCurrentSpawnNote);//scale y = note length
            if (currentIndexIntervalNote == _timeIntervalNote.Count - 1)
            {
                note.SetLastNote(true);
            }

        }

    }
    private Transform SpawnNoteWithOffSet(float oY, bool isHoldNote)
    {
        float unityUnit = 10;
        float realOffSetY = oY * unityUnit;
        // check if note length > 0.24f then spawn note is Hold Note else Normal note
        Transform randNote = NoteSpanwer.Instance.SpawnRandomOfSetY(realOffSetY, isHoldNote);
        randNote.gameObject.SetActive(true);
        return randNote;
    }
    void GetDataFormFile()
    {
        TextAsset intevalFile = DataTransferSenceData.GetSelectedSong().IntervalTextFile; // load data.txt
        string[] data = intevalFile.text.Split(","); // get data
        foreach (string item in data)
        {
            _timeIntervalNote.Add(float.Parse(item, System.Globalization.CultureInfo.InvariantCulture));//change string to float then add to timeIntervalNote
        }
    }



}

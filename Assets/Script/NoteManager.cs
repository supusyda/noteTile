using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _timeToNextNote;
    [SerializeField] private float _nextNoteTimer = 0;
    [SerializeField] private TextAsset textFile;
    [SerializeField] private List<float> timeIntervalNote = new List<float>();
    private float _pixelToUnitRatio = 0.01f;
    private int _currentIndexIntervalNote = 0;
    void Start()
    {
        textFile = Resources.Load("Text/interval") as TextAsset;
        string[] abc = textFile.text.Split(",");

        foreach (string item in abc)
        {
            timeIntervalNote.Add(float.Parse(item, System.Globalization.CultureInfo.InvariantCulture));

        }



        _timeToNextNote = 0;
        float oY = 0;
        Transform firstNote = SpawnNoteWithOfSet(oY);
        _timeToNextNote = timeIntervalNote[_currentIndexIntervalNote];
        ScaleObjectToDistance(_timeToNextNote, firstNote);
        timeIntervalNote.ForEach((eachNoteDistance) =>
        {
            oY += _timeToNextNote;
            Transform note = SpawnNoteWithOfSet(oY);
            SetTimeToNextNote();
            ScaleObjectToDistance(_timeToNextNote, note);

        });



    }

    private void Update()
    {

        // if (_timeToNextNote > _nextNoteTimer)
        // {
        //     _nextNoteTimer += Time.deltaTime;
        // }
        // else
        // {
        //     _nextNoteTimer = 0;
        //     SpawnNote();

        // }

    }
    private Transform SpawnNoteWithOfSet(float oY)
    {

        Transform randNote = NoteSpanwer.Instance.SpawnRandomOfSetY(oY * 10);
        // randNote.GetComponent<Note>().SetLength(_timeToNextNote * 10, _timeToNextNote);
        randNote.gameObject.SetActive(true);
        return randNote;
    }


    void ScaleObjectToDistance(float distant, Transform note)
    {
        // Calculate the distance between the two objects in Unity units
        // Debug.Log("ScaleObjectToDistance" + distant);
        float yDistance = distant * 10;

        // Convert 550 pixels to Unity units
        float targetLengthInUnits = 550 * _pixelToUnitRatio;

        // Scale object1's Y length based on the distance to object2
        Vector3 newScale = note.localScale;

        // Calculate the scale factor needed
        newScale.y = yDistance / targetLengthInUnits;

        note.localScale = newScale;
    }
    private void SetTimeToNextNote()
    {
        if (_currentIndexIntervalNote >= timeIntervalNote.Count - 1)
        {
            // _currentIndexIntervalNote = 0;
            Debug.Log("DONE");
            return;
        }

        _timeToNextNote = timeIntervalNote[++_currentIndexIntervalNote];
    }


}

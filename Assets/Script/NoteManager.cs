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
    private float normalNoteLengthMax = 0.24f;

    private int _currentIndexIntervalNote = 0;









    void Start()
    {
        textFile = Resources.Load("Text/interval") as TextAsset;
        string[] abc = textFile.text.Split(",");

        foreach (string item in abc)
        {
            timeIntervalNote.Add(float.Parse(item, System.Globalization.CultureInfo.InvariantCulture));

        }




        float offSetY = 0;
        _lengthOfCurrentSpawnNote = timeIntervalNote[_currentIndexIntervalNote];
        Transform firstNote = SpawnNoteWithOffSet(offSetY);
        ScaleObjectToDistance(_lengthOfCurrentSpawnNote, firstNote);
        timeIntervalNote.ForEach((eachNoteDistance) =>
        {
            offSetY += _lengthOfCurrentSpawnNote;
            SetTimeToNextNote();
            Transform note = SpawnNoteWithOffSet(offSetY);
            ScaleObjectToDistance(_lengthOfCurrentSpawnNote, note);

        });
    }

    private Transform SpawnNoteWithOffSet(float oY)
    {

        Transform randNote = NoteSpanwer.Instance.SpawnRandomOfSetY(oY * 10, IsHoldNote(_lengthOfCurrentSpawnNote));
        randNote.gameObject.SetActive(true);
        return randNote;
    }
    private bool IsHoldNote(float noteLength)
    {
        return noteLength > this.normalNoteLengthMax;
    }

    void ScaleObjectToDistance(float distant, Transform note)
    {
        // Calculate the distance between the two objects in Unity units
        // Debug.Log("ScaleObjectToDistance" + distant);
        float yDistance = distant * 10;

        // Convert 550 pixels to Unity units
        float targetLengthInUnits = 550 * _pixelToUnitRatio;

        // Scale object1's Y length based on the distance to object2
        // Vector3 newScale = note.localScale;

        // Calculate the scale factor needed
        float noteLengthScaleFactor = yDistance / targetLengthInUnits;

        note.GetComponent<Note>().SetLength(noteLengthScaleFactor);

    }
    private void SetTimeToNextNote()
    {
        if (_currentIndexIntervalNote >= timeIntervalNote.Count - 1) return;
        _lengthOfCurrentSpawnNote = timeIntervalNote[++_currentIndexIntervalNote];
    }
    // private float GetNoteLength()
    // {
    //     return _currentIndexIntervalNote + 1;
    // }


}

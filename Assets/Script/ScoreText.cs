using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    private TMP_Text score;
    private int _score = 0;
    void Awake()
    {
        score = GetComponent<TMP_Text>();
    }
    private void OnEnable()
    {
        EventDefine.onSuccessClickOnNote.AddListener(AddScore);
    }
    private void OnDisable()
    {
        EventDefine.onSuccessClickOnNote.RemoveListener(AddScore);
    }
    private void SetScoreTxt()
    {
        score.text = "Score:" + _score.ToString();
    }
    private void AddScore()
    {
        _score++;
        SetScoreTxt();
    }
    // Update is called once per frame

}

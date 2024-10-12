using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ScoreEffect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Sprite[] fg, bg;
    [SerializeField] Transform fgTrans, bgTrans;
    void Awake()
    {

    }

    void OnEnable()
    {
        EventDefine.onSuccessClickOnNote.AddListener(Show);
        EventDefine.onStart.AddListener(ActiveScoreEffect);
    }
    void OnDisable()
    {
        EventDefine.onSuccessClickOnNote.RemoveListener(Show);
        EventDefine.onStart.RemoveListener(ActiveScoreEffect);
    }
    void Show()
    {
        var fgIndex = Random.Range(0, fg.Length);
        var bgIndex = Random.Range(0, bg.Length);
        fgTrans.GetComponent<Image>().sprite = fg[fgIndex];
        bgTrans.GetComponent<Image>().sprite = bg[bgIndex];
    }
    void ActiveScoreEffect()
    {
        bgTrans.gameObject.SetActive(true);
        fgTrans.gameObject.SetActive(true);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoPanel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform text;
    void OnEnable()
    {
        EventDefine.onStartGame.AddListener(Hide);
    }
    private void OnDisable()
    {
        EventDefine.onStartGame.RemoveListener(Hide);
    }

    private void Hide()
    {
        text.gameObject.SetActive(false);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float progressToTriggerEvent;

    bool isTrigger = false;

    private void OnEnable()
    {
        EventDefine.onReachProgression.AddListener(OnReachProgression);
    }
    private void OnDisable()
    {
        EventDefine.onReachProgression.RemoveListener(OnReachProgression);
    }

    private void OnReachProgression(float progress)
    {
        if (progress < progressToTriggerEvent || isTrigger == true) return;
        isTrigger = true;
        GetComponent<IRotate>().Rotate();
    }
}

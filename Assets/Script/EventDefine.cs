using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventDefine
{
    // Start is called before the first frame update
    public static UnityEvent onSuccessClickOnNote = new();
    public static UnityEvent<Transform> onMissClickOnNote = new();
    public static UnityEvent onLose = new();


}

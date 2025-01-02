using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCheckGameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.parent.TryGetComponent<Note>(out Note note) && note.IsAlreadyInteract == false)
        {

            EventDefine.onLose.Invoke();
        }
    }
}

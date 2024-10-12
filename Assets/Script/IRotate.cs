using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IRotate : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float rotateSpeed;
    bool canRotate = false;

    void Update()
    {
        if (!canRotate) return;
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
    public void Rotate()
    {
        canRotate = true;
    }
}

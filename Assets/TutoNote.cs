using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoNote : MonoBehaviour, IClick
{
    // Start is called before the first frame update
    public virtual void Click()
    {
        EventDefine.onStartGame?.Invoke();
        transform.parent.gameObject.SetActive(false);

    }
}

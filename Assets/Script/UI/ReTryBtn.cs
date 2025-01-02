using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReTryBtn : BtnBase
{
    // Start is called before the first frame update
    protected override void OnClick()
    {
        base.OnClick();
        TransistionScene.ChangeScene.Invoke("Game", "Circle");
    }
}

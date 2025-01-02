using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLevelBtn : BtnBase
{
    override protected void OnClick()
    {
        base.OnClick();
        TransistionScene.ChangeScene.Invoke("SongSelectedScene", "Circle");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : Note
{

    [SerializeField] private Transform holdIndicator;
    public float count = 0;
    private bool _canBeHold = true;
    private float _maxHoldScaleY = 2.25f;
    override protected void Update()
    {
        base.Update();
        if (IsHoldAble())//mouse hold
        {
            count += Time.deltaTime * noteSpeed;
            OnHold();
        }
        if (isClick && Input.GetMouseButtonUp(0))//mouse release
        {
            _canBeHold = false;
            Fade();
        }
    }
    protected override void OnClick()
    {
        if (isClick) return;
        EventDefine.onSuccessClickOnNote?.Invoke();
        isClick = true;
    }
    private bool IsHoldAble()
    {
        return isClick && Input.GetMouseButton(0) && _canBeHold == true;
    }
    protected override void Init()
    {
        base.Init();
        _canBeHold = true;
        ResetHoldIndicator();

    }
    private void ResetHoldIndicator()
    {
        holdIndicator.localScale = new Vector3(holdIndicator.localScale.x, 0, 0);
    }
    private void OnHold()
    {
        if (count >= _maxHoldScaleY)
        {
            _canBeHold = false;
            Fade();
            return;
        }
        holdIndicator.localScale = new Vector3(holdIndicator.localScale.x, count, 0);
    }

}

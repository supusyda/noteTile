using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera camera;
    [SerializeField] ContactFilter2D contactFilter;
    private bool _isLose = false;
    private bool _isStartGame = false;

    void OnEnable()
    {
        EventDefine.onLose.AddListener(OnLose);
        EventDefine.onStartGame.AddListener(() =>
        {
            _isStartGame = true;
        });
    }
    private void OnLose()
    {
        _isLose = true;
    }

    private void Update()
    {
        if (_isLose) return;
        if (Input.GetMouseButtonDown(0))
        {
            DrawRayAtMouse();
        }
    }

    void DrawRayAtMouse()
    {
        List<RaycastHit2D> results = new();
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        float distance = Mathf.Infinity;
        Physics2D.Raycast(mousePos, Vector2.zero, contactFilter, results, distance);//fire ray cast at mouse

        if (results.Count <= 0 && _isStartGame == true) //game start but not hit any note
        {
            FireRayMissNote(mousePos);
            return;
        }
        else if (results.Count == 0 && _isStartGame == false) return; // game havent start yet


        //Game start and hit note
        RaycastHit2D raycastHit2Dhit = results[0];
        // if (raycastHit2Dhit == null) return;

        IClick clickAble = raycastHit2Dhit.collider.transform.parent.GetComponent<IClick>();
        if (clickAble != null) clickAble.Click();
    }

    void FireRayMissNote(Vector3 mousePos)
    {
        float range = 100;
        RaycastHit2D hitRight = Physics2D.Raycast(mousePos, Vector2.right, range, contactFilter.layerMask);//FIRE LEFT RAY
        RaycastHit2D hitLeft = Physics2D.Raycast(mousePos, Vector2.left, range, contactFilter.layerMask);//fIRE RIGHT RAY
        Transform noteHitRay = null;

        if (hitRight.collider == null && hitLeft.collider == null) return;
        // Check if the ray to the right hit something

        if (hitRight.collider != null)
        {
            noteHitRay = hitRight.collider.transform.parent;
        };

        // Check if the ray to the left hit something
        if (hitLeft.collider != null)
        {
            noteHitRay = hitLeft.collider.transform.parent;
        }

        if (noteHitRay.GetComponent<Note>().GetIsClick() == true) return; // miss the note that already be hit is oke if not been hit "LOSE"

        //LOSE
        EventDefine.onMissClickOnNote?.Invoke(noteHitRay);
        EventDefine.onLose?.Invoke();

        return;
    }

}

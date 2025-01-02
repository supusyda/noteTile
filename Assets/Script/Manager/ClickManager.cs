using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Camera camera;
    [SerializeField] ContactFilter2D contactFilter;





    private void Update()
    {
        if (GameManager.Instance.GetGameState() != GameState.Start && GameManager.Instance.GetGameState() != GameState.Tutorial) return;
        // Debug.Log(GameManager.Instance.GetGameState());

        if (Input.GetMouseButtonDown(0))
        {
            DrawRayAtMouse();
        }
    }

    void DrawRayAtMouse()
    {
        List<RaycastHit2D> hitNote = new();
        Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        Physics2D.Raycast(mousePos, Vector2.zero, contactFilter, hitNote);//fire ray cast at mouse

        if (hitNote.Count <= 0 && GameManager.Instance.GetGameState() == GameState.Start) //game start but not hit any note at mouse click position
        {
            FireRayMissNote(mousePos); // check for the missing note
            return;
        }
        else if (hitNote.Count == 0 && GameManager.Instance.GetGameState() != GameState.Start) return; // game havent start yet


        //Game start and hit note
        RaycastHit2D raycastHit2Dhit = hitNote[0];
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

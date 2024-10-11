using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera camera;
    [SerializeField] ContactFilter2D contactFilter;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            List<RaycastHit2D> results = new();

            Vector2 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            float distance = Mathf.Infinity;
            Physics2D.Raycast(mousePos, Vector2.zero, contactFilter, results, distance);
            if (results.Count <= 0)
            {
                Debug.Log("No hit");
                float range = 100;
                RaycastHit2D hitRight = Physics2D.Raycast(mousePos, Vector2.right, range, contactFilter.layerMask);
                RaycastHit2D hitLeft = Physics2D.Raycast(mousePos, Vector2.left, range, contactFilter.layerMask);

                if (hitRight.collider != null)
                {
                    Debug.Log("Hit to the right: " + hitRight.collider.transform.parent?.name);
                    var missNotePos = hitRight.collider.transform.parent.position;
                    var missNoteLength = hitRight.collider.transform.parent.localScale.y;

                    EventDefine.onMissClickOnNote?.Invoke(hitRight.collider.transform.parent);
                }

                // Check if the ray to the left hit something
                if (hitLeft.collider != null)
                {
                    Debug.Log("Hit to the left: " + hitLeft.collider.transform.parent?.name);
                    EventDefine.onMissClickOnNote?.Invoke(hitLeft.collider.transform.parent);
                }
                return;
            };
            RaycastHit2D raycastHit2Dhit = results[0];

            if (raycastHit2Dhit == null) return;

            IClick clickAble = raycastHit2Dhit.collider.transform.parent.GetComponent<IClick>();
            if (clickAble != null) clickAble.Click();

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
  // Start is called before the first frame update
  [SerializeField] private float noteLength;
  [SerializeField] private float ID;
  [SerializeField] public float lengthNeed;


  [SerializeField] private float noteSpeed;
  void Start()
  {
    float length = transform.Find("Model").GetComponent<SpriteRenderer>().bounds.size.y;
    // Debug.Log("" + length);




  }

  public void SetLength(float length, float id)
  {
    this.transform.localScale = new Vector3(this.transform.localScale.x, length, 1);
    ID = id;
  }
  // Update is called once per frame
  void Update()
  {
    transform.position = new Vector3(transform.position.x, transform.position.y - noteSpeed * Time.deltaTime, 0);
  }
}

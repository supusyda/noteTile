using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Note : MonoBehaviour, IClick
{
  // Start is called before the first frame update

  // [SerializeField] private float ID;
  // [SerializeField] public float lengthNeed;

  [SerializeField] private SpriteRenderer model;
  [SerializeField] protected bool isClick = false;
  [SerializeField] protected float noteSpeed;
  protected static bool firstNotePlayed = false;


  public void SetLength(float length)
  {

    this.transform.localScale = new Vector3(this.transform.localScale.x, length, 1);
  }
  // Update is called once per frame
  protected virtual void Awake()
  {

  }
  private void OnEnable()
  {
    Init();
    EventDefine.onLose.AddListener(OnLose);
    EventDefine.onStartGame.AddListener(OnStartGame);
  }
  private void OnDisable()
  {
    EventDefine.onLose.RemoveListener(OnLose);
    EventDefine.onStartGame.RemoveListener(OnStartGame);
  }
  private void OnStartGame()
  {
    this.noteSpeed = 10;
  }
  protected virtual void Update()
  {
    transform.position = new Vector3(transform.position.x, transform.position.y - noteSpeed * Time.deltaTime, 0);
  }

  protected virtual void OnLose()
  {
    noteSpeed = 0;
  }
  protected virtual void OnClick()
  {
    if (isClick) return;
    EventDefine.onSuccessClickOnNote?.Invoke();
    Fade();
    isClick = true;


    if (NoteManager.Instance.FirstNotePlayed == true) return;
    EventDefine.onStart?.Invoke();
    NoteManager.Instance.FirstNotePlayed = true;

  }
  public void Click()
  {
    OnClick();
  }
  protected virtual void Fade()
  {
    model.color = new Color(model.color.r, model.color.g, model.color.b, .7f);
  }
  protected virtual void Init()
  {
    model.color = new Color(model.color.r, model.color.g, model.color.b, 1);
    isClick = false;


  }
  public bool GetIsClick()
  {
    return isClick;
  }
}

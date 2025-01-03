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
  [SerializeField] protected bool isClicked = false;
  [SerializeField] protected float noteSpeed;
  public bool IsAlreadyInteract { get; private set; }
  protected bool isLastNote = false;
  protected static bool firstNotePlayed = false;
  private readonly float _pixelToUnitRatio = 0.01f;
  private readonly float noteImgOriginalPixelLength = 550;
  private static readonly float _normalNoteLengthMax = 0.24f;



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
    if (isClicked) return;

    EventDefine.onSuccessClickOnNote?.Invoke();
    Fade();
    isClicked = true; // can't click again
    IsAlreadyInteract = true;
    if (!isLastNote) return;
    EventDefine.OnDoneSong?.Invoke();
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
    isClicked = false;
    IsAlreadyInteract = false;


  }
  public bool GetIsClick()
  {
    return isClicked;
  }
  public void SetLastNote(bool isLastNote)
  {
    this.isLastNote = isLastNote;
  }
  public void ScaleToDataLenght(float distant)
  {
    float yDistance = distant * 10;

    // Convert 550 pixels to Unity units
    float myTargetLengthInUnits = noteImgOriginalPixelLength * _pixelToUnitRatio;




    // Calculate the scale factor needed
    float noteLengthScaleFactor = yDistance / myTargetLengthInUnits;

    this.transform.localScale = new Vector3(this.transform.localScale.x, noteLengthScaleFactor, 1);
  }

  public static bool IsHoldNote(float length)
  {
    return length > _normalNoteLengthMax;
  }
}

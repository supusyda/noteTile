using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class ProgressBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _timeToDrain = 0.25f;
    private float _target;
  [SerializeField]  private Image fillImage;
    private void OnEnable()
    {

       
        EventDefine.onSuccessClickOnNote.AddListener(AddBar);
    }
    private void OnDisable()
    {
        EventDefine.onSuccessClickOnNote.RemoveListener(AddBar);


        // holder.gameObject.SetActive(false);

    }

    IEnumerator DrainHealthBar()
    {

        float elapedTime = 0;
        float newValue;
        while (elapedTime < _timeToDrain)
        {
    

            elapedTime += Time.deltaTime;
            newValue = Mathf.Lerp(fillImage.fillAmount, _target, elapedTime / _timeToDrain);
            fillImage.fillAmount = newValue;

            // fillImage.color = Color.Lerp(fillImage.color, new Color(fillImage.color.r, (fillImage.fillAmount / 1) - 0.1f, fillImage.color.b, 1), elapedTime / _timeToDrain);

            yield return null;
        }
    }
    public void AddBar()
    {
        // show health bar when being hit


        _target += 1 / 53f;
        StartCoroutine(DrainHealthBar());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReTryBtn : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        SceneManager.LoadSceneAsync("Game");
    }
}

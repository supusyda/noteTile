using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform gameOverPanel;
    void OnEnable()
    {
        EventDefine.onLose.AddListener(ShowPanel);
        EventDefine.OnDoneSong.AddListener(ShowPanel);
    }
    private void OnDisable()
    {
        EventDefine.onLose.RemoveListener(ShowPanel);
        EventDefine.OnDoneSong.RemoveListener(ShowPanel);
    }
    void ShowPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
    }
}

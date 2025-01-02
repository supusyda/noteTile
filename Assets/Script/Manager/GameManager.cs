using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Init,
    Tutorial,
    Start,
    GameOver,

}
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    // private bool _isLose = false;
    // public bool IsLose { get => _isLose; }
    // private bool _isStartGame = false;
    // public bool IsStartGame { get => _isStartGame; }
    private static GameManager _instance;
    public static GameManager Instance { get => _instance; }
    private GameState _gameState;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void OnEnable()
    {
        EventDefine.onLose.AddListener(OnLose);
        EventDefine.onStartGame.AddListener(OnStartGame);

    }

    private void OnDisable()
    {
        EventDefine.onLose.RemoveListener(OnLose);
        EventDefine.onStartGame.RemoveListener(OnStartGame);
    }
    private void OnLose()
    {
        ChangeGameState(GameState.GameOver);
    }
    private void OnStartGame()
    {
        ChangeGameState(GameState.Start);
    }
    void Start()
    {
        ChangeGameState(GameState.Tutorial);
    }
    public void ChangeGameState(GameState newState)
    {
        if (newState == _gameState)
            return;

        switch (newState)
        {
            case GameState.Init:
                break;
            case GameState.Start:
                break;
            case GameState.Tutorial:
                break;
            case GameState.GameOver:
                break;
            default:
                break;
        }
        _gameState = newState;
    }
    public GameState GetGameState()
    {
        return _gameState;
    }
}

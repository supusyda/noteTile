using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TransistionScene : MonoBehaviour
{

    [DoNotSerialize] public string GAME_SCENE = "Game";
    public static UnityEvent<string, string> ChangeScene = new UnityEvent<string, string>();


    // public string MAIN_MENU_SCENE = "Menu";




    public static TransistionScene Instance { get; private set; }
    [SerializeField] List<Transform> transitions;



    private void Awake()
    {
        // Ensure only one instance of LevelManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.gameObject);
            // Keep across scene loads
        }
        else
        {
            Destroy(gameObject);
        }

        // Load all level files at the start
        // transitions

        LoadTransition();
        // LoadAllLevelFiles();

    }

    void OnEnable()
    {
        ChangeScene.AddListener(BtnUITrans);
    }
    void OnDisable()
    {
        ChangeScene.RemoveListener(BtnUITrans);
    }

    void LoadTransition()
    {
        Transform allTransition = transform.Find("Transition");
        foreach (Transform transition in allTransition)
        {
            transitions.Add(transition);
        }
    }



    private void BtnUITrans(string sceneName, string transitionName)
    {
        TransitionToScene(sceneName, transitionName);
    }


    // Load all level files ending with level.json


    // Get all available levels


    public void TransitionToScene(string sceneName, string transitionName, Action callback = null)
    {
        StartCoroutine(LoadSceneAsync(sceneName, transitionName, callback));
    }

    private IEnumerator LoadSceneAsync(string sceneName, string transitionName, Action callback
     = null)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;
        ITransition transition = GetTransitionByName(transitionName);

        // Optionally, show a loading screen or progress here
        yield return transition.TransitionIn();
        while (asyncLoad.progress < 0.9f)
        {

            yield return null;
        }
        asyncLoad.allowSceneActivation = true;
        // yield return new WaitUntil(() => asyncLoad.isDone);
        yield return null;

        callback?.Invoke();
        yield return transition.TransitionOut();

    }
    ITransition GetTransitionByName(string name)
    {

        return transitions.First(t => t.name == name).GetComponent<ITransition>();
    }

    // Save level progress (customize as needed)







}

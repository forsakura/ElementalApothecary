using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class SceneManger : MonoBehaviour,ISavable
{
    private static SceneManger instance;

    [Header("组件")]
    public CanvasGroup fadeCanvas;
    [HideInInspector]public Transform playerTrans;
    public GameObject playerBar;

    public string currentScene;
    private string sceneToLoad;
    private Vector3 posToGo;
    [Header("事件监听")]
    public SceneLoadEventSO sceneLoadEvent;
    public SceneLoadEventSO startNewGame;
    public SceneLoadEventSO backToMenu;

    [Header("状态")]
    public bool isFade;
    public float fadeDir;
    public static SceneManger Instance
    {
        get { return instance; }
    }
    private void Awake() 
    {
       if (instance != null)
            Destroy(gameObject);
        else
            instance = this;

        
    }
    private void Start() 
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;

        SceneManager.LoadSceneAsync("Menu",LoadSceneMode.Additive);
        currentScene = "Menu";
    }


    private void Update() 
    {
       
    }

   private void OnEnable() 
   {
        sceneLoadEvent.LoadSceneEvent += OnSceneLoadEvent;
        startNewGame.LoadSceneEvent += OnSceneLoadEvent;
        backToMenu.LoadSceneEvent += OnSceneLoadEvent;


        ISavable savable = this;
        savable.RegisterSaveData();
   }
   private void OnDisable() 
   {
        sceneLoadEvent.LoadSceneEvent -= OnSceneLoadEvent;
        startNewGame.LoadSceneEvent -= OnSceneLoadEvent;
        backToMenu.LoadSceneEvent -= OnSceneLoadEvent;
        ISavable savable = this;
        savable.UnRegisterSaveData();
   }


    private void OnSceneLoadEvent(string from, string to, Vector3 posTo)
    {

        sceneToLoad = to;
        posToGo = posTo;
        if(!isFade)
        {
            StartCoroutine(LoadScene(from,to,posTo));         
        }
    }
    public IEnumerator LoadScene(string from, string to, Vector3 posTo)
    {   
        OnFade();
        yield return Fade(1);
        
        
        if( from != null)
        yield return SceneManager.UnloadSceneAsync(from);

        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);
        
        playerTrans.position = posTo;

        GetActiveScene();
         
        yield return Fade(0);
        AfterFade();
        LoadMenu();

    }

    IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvas.gameObject.SetActive(true);

        fadeCanvas.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvas.alpha - targetAlpha)/fadeDir;

        while(!Mathf.Approximately(fadeCanvas.alpha,targetAlpha))
        {
            fadeCanvas.alpha = Mathf.MoveTowards(fadeCanvas.alpha,targetAlpha,speed*Time.deltaTime);
            yield return null;
        }

        fadeCanvas.blocksRaycasts = false;
        fadeCanvas.gameObject.SetActive(false);
        isFade = false;
}
    public void OnFade()
    {
       
        
        playerTrans.gameObject.GetComponent<PlayerControll>().inputAction.Disable();
        playerTrans.gameObject.GetComponent<PlayerControll>().rb.velocity = Vector2.zero;
        playerTrans.gameObject.SetActive(false);
        playerBar.SetActive(false);
        
           
        
    }
    public void AfterFade()
    {
        playerTrans.gameObject.GetComponent<PlayerControll>().inputAction.Enable();
        playerTrans.gameObject.SetActive(true);
        playerBar.SetActive(true);
    }

    public void LoadMenu()
    {
        if(currentScene == "Menu")
        {
            OnFade();
        }
        else
        {
            AfterFade();
        }
    }

    public void GetActiveScene()
    {
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);
        currentScene = newScene.name;
    }

    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveData(Data data)
    {
        string key = GetDataID().ID + "Scene";

        // if (!data.stringDic.ContainsKey(key))
        // {
        //     data.stringDic.Add(key, currentScene);
        // }
        // else
        // {
        //     data.stringDic[key] = currentScene;
        // }
    }

    public void LoadData(Data data)
    {
        // var playerID = playerTrans.GetComponent<DataDefinition>().ID;
        // if (data.characterPosDict.ContainsKey(playerID))
        // {
        //     posToGo = data.characterPosDict[playerID].ToVector3();
        //     sceneToLoad = data.stringDic[GetDataID().ID + "Scene"];

        //     posToGo = data.characterPosDict[playerID].ToVector3();

        //     OnSceneLoadEvent(currentScene, sceneToLoad, posToGo);
        // }

      
    }
}

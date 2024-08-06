using System;
using System.Collections;
using System.Collections.Generic;
using ProjectBase.UI;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Button start;
    public Button load;
    public Button tk;
    public Button setting;
    public Button exit;
    public SceneLoadEventSO startNew;
    public Vector3 posToGo;
    private void Awake() 
    {
        /*start.onClick.AddListener(OnStart);
        load.onClick.AddListener(OnLoad);
        tk.onClick.AddListener(OnTK);
        setting.onClick.AddListener(OnSetting);
        exit.onClick.AddListener(OnExit);*/
    }


    

    
    private void OnStart()
    {
        // UIManager.Instance.OnStartNewGame("Home");
        // SceneMgr.Instance.UnLoadSceneAsync("Menu");
        startNew.RaiseLoadScenetEvent("Menu","Home",posToGo);
        
    }
    /*private void OnLoad()
    {
        UIManager.Instance.OnLoadGame();
    }
    private void OnTK()
    {
        UIManager.Instance.OnThanks();
    }
    private void OnSetting()
    {
        UIManager.Instance.OnSetting();
    }
    private void OnExit()
    {
        UIManager.Instance.OnExitGame();
    }*/
}

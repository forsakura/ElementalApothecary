using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using FrameWork;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoSingleton<UIManager>
{
    /// <summary>
    /// �������ĸ�����
    /// </summary>
    public GameObject ui;
    
    /// <summary>
    /// ��ʼ��Ϸ���?
    /// </summary>
    public GameObject startPanel;
    
    /// <summary>
    /// ������Ϸ���?
    /// </summary>
    public GameObject loadPanel;
    
    /// <summary>
    /// ���ý���
    /// </summary>
    public GameObject settingPanel;
    
    /// <summary>
    /// ��л���?
    /// </summary>
    public GameObject thanksPanel;
    
    /// <summary>
    /// ���ؽ���
    /// </summary>
    public GameObject loadingPanel;

    public GameObject fightUI;
    
    /// <summary>
    /// �������ļ���
    /// </summary>
    public List<GameObject> panels;

    protected override void Init()
    {
        base.Init();
        fightUI.SetActive(false);
       // SceneMgr.Instance.LoadNextScene("Menu");
        panels.Add(startPanel);
        panels.Add(loadPanel);
        panels.Add(settingPanel);
        panels.Add(thanksPanel);
        panels.Add(loadingPanel);
    }

    private void OnEnable()
    {
        DontDestroyOnLoad(Instance);
        DontDestroyOnLoad(ui);
    }

    private void Start()
    {
        //ShowCurrentCanvas(startPanel);
    }

    /// <summary>
    /// ��ʼ��ť�����¼�
    /// </summary>
    /// <param name="sceneName">������</param>
    public void OnStartNewGame(string sceneName)
    {
        HideAllCanvas();
        SceneMgr.Instance.LoadNextScene(sceneName);
    }

    /// <summary>
    /// ������Ϸ��ť�����¼�
    /// </summary>
    public void OnLoadGame()
    {
        ShowCurrentCanvas(loadPanel);
    }

    /// <summary>
    /// ���ð�ť�����¼�
    /// </summary>
    public void OnSetting()
    {
        ShowCurrentCanvas(settingPanel);
    }

    /// <summary>
    /// ��л��ť�����¼�
    /// </summary>
    public void OnThanks()
    {
        ShowCurrentCanvas(thanksPanel);
    }

    /// <summary>
    /// �˳���ť�����¼�
    /// </summary>
    public void OnExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    /// <summary>
    /// ������Ϸ��ť�����¼�
    /// </summary>
    public void OnBackGame()
    {
        HideAllCanvas();
    }

    /// <summary>
    /// �������˵���ť�����¼�
    /// </summary>
    public void OnBackToMenu()
    {
        ShowCurrentCanvas(startPanel);
    }

    /// <summary>
    /// ��ʾ��ǰ��Ҫ��ʾ�����?
    /// </summary>
    /// <param name="curCanvas">��ʾ�����?/param>
    private void ShowCurrentCanvas(GameObject curCanvas)
    {
        foreach (var VARIABLE in panels)
        {
            if (VARIABLE != curCanvas)
            {
                VARIABLE.SetActive(false);
            }
            else
            {
                VARIABLE.SetActive(true);
            }
        }
    }

    /// <summary>
    /// �����������?
    /// </summary>
    private void HideAllCanvas()
    {
        foreach (var VARIABLE in panels)
        {
            VARIABLE.SetActive(false);
        }
    }
}

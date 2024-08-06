using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadButton : MonoBehaviour
{
    public Button loadBtn;
    public VoidEvent_SO loadEvent;
    public string savePath;

    private void Awake() 
    {
        loadBtn.onClick.AddListener(OnLoad);
    }

    private void OnLoad()
    {
        //SaveLoadManager.ReadSavedData(savePath);
        loadEvent.RaiseEvent();
    }
}

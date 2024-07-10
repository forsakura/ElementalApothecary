using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DeletButton : MonoBehaviour
{
    public Button deletBtn;
    public StringEvent_SO deletEvent;
    public string savePath;

    private void Awake() 
    {
        deletBtn.onClick.AddListener(OnDelet);
    }

    private void OnDelet()
    {
        deletEvent.RaiseEvent(savePath);
    }
}

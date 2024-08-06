using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TrackButton : MonoBehaviour
{
    [Tooltip("是否追踪的按钮")]public Button trackBtn;
    [Tooltip("追踪的文本")]public Text trackText;

    public MessionButton messionButton;

    private void Awake() 
    {
        trackBtn.onClick.AddListener(BtnClick);
    }

    private void Update() 
    {
        if(messionButton.messionDataSO.isTrack)
        {
            trackText.text = "追踪中";
            messionButton.btnImage.color = new Color(0,0,0,0.55f);
        }
        else
        {
            trackText.text = "追踪"; 
            messionButton.btnImage.color = new Color(1,1,1);
        } 
    }

    private void BtnClick()
    {
        if(messionButton.messionDataSO.isTrack)
        {
            messionButton.messionDataSO.isTrack = false;
        }
        else
        {
            messionButton.messionDataSO.isTrack = true; 
        } 
 
        MessionSystem.Instance.ReTrack(messionButton);
        
    }
}

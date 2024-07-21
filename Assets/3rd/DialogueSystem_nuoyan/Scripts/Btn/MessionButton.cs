using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MessionButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public Button messionBtn;
    public Text messionName;
    public Image btnImage;
    [HideInInspector]public MessionDataSO messionDataSO;
    
    private void Awake() 
    {
        messionBtn.onClick.AddListener(BtnClick);
    }


    private void BtnClick()
    {
        MessionSystem.SyncMessionInformation(messionDataSO);


        MessionSystem.Instance.ClearTrackBtn();
        MessionSystem.Instance.InstanceTrackBtn(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1.1f,1.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = new Vector3(1f,1f);
    }


}

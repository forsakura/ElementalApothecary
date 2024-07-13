using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessionPanel : MonoBehaviour
{
   [Tooltip("任务名称")]public Text messionName;
   [Tooltip("任务地点")]public Text messionPosition;
   [Tooltip("任务描述")]public Text messionDetails;
   [Header("------")]
   [Tooltip("生成任务按钮位置")]public Transform messionBtnParent;
   [Tooltip("生成任务按钮位置")]public Transform trackBtnParent;
   [Header("------")]
   [Tooltip("任务按钮，点击显示任务信息")]public MessionButton messionButton;
   [Tooltip("追踪任务按钮")]public TrackButton trackButton;
   [Header("------")]
   [Tooltip("有任务显示")]public GameObject haveMession;
   [Tooltip("无任务显示")]public GameObject noMession;

   public DisplayMession displayMession;
  



    private void OnEnable() 
    {
        MessionSystem.Instance.FirstSelected();
        MessionSystem.Instance.ClearTrackBtn();
    }
}

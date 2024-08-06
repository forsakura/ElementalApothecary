using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessionSystem : Utilities.Singleton<MessionSystem>,ISavable
{
    public MessionPanel messionPanel;

    [Tooltip("当前执行任务")]public MessionButton currentMession;

    [Tooltip("已接取的任务")]public List<MessionDataSO> messionDataSOList = new List<MessionDataSO>();

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable() 
    {
        ISavable savable = this;
        savable.RegisterSaveData();
    }
    private void OnDisable() 
    {
        ISavable savable = this;
        savable.UnRegisterSaveData();
    }

    public void InstanceMessionBtn(List<MessionDataSO> messionDataSOList)//生成任务按钮
    {
        if(messionDataSOList == null)
            return;

        MessionButton messionButton;

        for (int i = 0; i < messionDataSOList.Count; i++)
        {
            messionButton = Instantiate(messionPanel.messionButton,messionPanel.messionBtnParent);
            messionButton.messionDataSO = messionDataSOList[i];
            messionButton.messionName.text = messionDataSOList[i].messionName;


            //加载存档时执行
            if(messionDataSOList[i].isTrack)
            {
                ReTrack(messionButton);
            }
            
        }
        

    }

    public static void SyncMessionInformation(MessionDataSO messionDataSO)//同步任务信息  点击时执行
    {
        Instance.messionPanel.messionName.text = messionDataSO.messionName;
        Instance.messionPanel.messionPosition.text = messionDataSO.messionPosition;
        Instance.messionPanel.messionDetails.text = messionDataSO.messionDetails;
    }

    public void FirstSelected()//当无追踪任务时，默认选择第一项任务，并且同步信息  没有任务时显示无任务面板
    {
        if(messionPanel.messionBtnParent.childCount > 0)
        {
            messionPanel.haveMession.SetActive(true);
            messionPanel.noMession.SetActive(false);
           
   
            
            GameObject mession = messionPanel.messionBtnParent.GetChild(0).gameObject;
            if(currentMession == null)
            {
                EventSystem.current.SetSelectedGameObject(mession);
                SyncMessionInformation(mession.GetComponent<MessionButton>().messionDataSO);
                InstanceTrackBtn(mession.GetComponent<MessionButton>());
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(currentMession.gameObject);
                SyncMessionInformation(currentMession.messionDataSO);
                InstanceTrackBtn(currentMession);
            }
                 
        }
        else
        {
            messionPanel.haveMession.SetActive(false);
            messionPanel.noMession.SetActive(true);
            
        }
        
    }

    public void InstanceTrackBtn(MessionButton messionButton)//点击时生成追踪按键，并将当前的messionBtn传递给TrackBtn
    {
        TrackButton trackButton;
        trackButton = Instantiate(messionPanel.trackButton,messionPanel.trackBtnParent);
        trackButton.messionButton = messionButton; 
    }


    public void ClearTrackBtn()
    {
        TrackButton[] trackButtons = messionPanel.trackBtnParent.GetComponentsInChildren<TrackButton>();

        foreach (var trackButton in trackButtons)
        {
            if(trackButtons.Length < 10)
                return;

            Destroy(trackButton.gameObject);
        }
    }

    public void ReTrack(MessionButton messionButton)//创建第三者当前执行任务  实现每次只能执行一个任务
    {
        currentMession = messionButton;

        if(currentMession.messionDataSO.isTrack)//有任务，且在追踪时，才会显示
        {
            messionPanel.displayMession.gameObject.SetActive(true);
            messionPanel.displayMession.messionPos.text = currentMession.messionDataSO.messionPosition;
        }
        else
        {
            messionPanel.displayMession.gameObject.SetActive(false);
        }
        

        for (int i = 0; i < messionPanel.messionBtnParent.childCount; i++)//循环所有的任务，如果不是正在执行的任务则设置 isTrack = false
        {
            if(messionPanel.messionBtnParent.GetChild(i).GetComponent<MessionButton>() != currentMession)   
                messionPanel.messionBtnParent.GetChild(i).GetComponent<MessionButton>().messionDataSO.isTrack = false; 
        }
        
    }

    public void GetSaveData(Data data)
    {
       data.messionDataSOList = messionDataSOList;
    }

    public void LoadData(Data data)
    {
        messionDataSOList = data.messionDataSOList;

        InstanceMessionBtn(messionDataSOList);
    }
    
}

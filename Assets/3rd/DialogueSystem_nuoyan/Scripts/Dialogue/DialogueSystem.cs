using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using Utilities;

public class DialogueSystem : Singleton<DialogueSystem>,ISavable
{
    [Tooltip("对话面板")]public DialoguePanel dialoguePanel;
    [Tooltip("对话数据树，互动时需要覆写此")]public DialogueTreeSO dialogueTree;
    [HideInInspector][Tooltip("当前处于第几句对话")]public int currentSpeechCount;
    [HideInInspector][Tooltip("当前对话片段")]public DialogueNode currentDialogueNode;//从对话数据列表中获得单个对话数据

    protected override void Awake() 
    {
        base.Awake();

        if (dialogueTree != null)
        {
            currentDialogueNode = dialogueTree.dialogueNodeList[0];

            dialogueTree.Initialization();

            SyncDialogueInformation(currentSpeechCount);

            SwitchPosition();
        }
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
    
    public void SyncDialogueInformation(int speechCount)//同步对话信息
    {
        if(currentDialogueNode.dialogueList.Count > 0)
        {
            dialoguePanel.speakerNameLeft.text = currentDialogueNode.speakerNameLeft;
            dialoguePanel.speakerNameRight.text = currentDialogueNode.speakerNameRight;
            
            dialoguePanel.rowImageLeft.sprite = currentDialogueNode.speakerLeft;
            dialoguePanel.rowImageRight.sprite = currentDialogueNode.speakerRight;

            dialoguePanel.speech.text = currentDialogueNode.dialogueList[speechCount].speech;
        }
    }

    public static void UpdateDialogueSpeech()//更新对话
    {
        Instance.currentSpeechCount++;
        if (Instance.currentSpeechCount >= Instance.currentDialogueNode.dialogueList.Count)
        {
            Instance.currentSpeechCount = 0;
        }

        Instance.SyncDialogueInformation(Instance.currentSpeechCount);
        Instance.InstanceChooseBtn(Instance.dialoguePanel.chooseParent);
        Instance.SwitchPosition();
        
    }

    public void InstanceChooseBtn(Transform transform)//生成选项
    {
        ChooseButton chooseButton;
        if (dialoguePanel.chooseButtonPre != null) // 判空逻辑
        {
            
            for (int i = 0; i < currentDialogueNode.dialogueList[currentSpeechCount].chooseList.Count; i++)
            {
                chooseButton = Instantiate(dialoguePanel.chooseButtonPre, transform);
                chooseButton.chooseText.text = currentDialogueNode.dialogueList[currentSpeechCount].chooseList[i].chooseBtnText;

                chooseButton.ID = currentDialogueNode.dialogueList[currentSpeechCount].chooseList[i].ID;


                //循环对话数据中的任务数量，添加到对应的对话选项按钮中
                for(int messionCount = 0 ; messionCount < currentDialogueNode.dialogueList[currentSpeechCount].chooseList[i].messionList.Count; messionCount++)//传入任务信息到按钮中
                {
                    chooseButton.messionDataSOList.Add(currentDialogueNode.dialogueList[currentSpeechCount].chooseList[i].messionList[messionCount].messionDataSO);

                }

                dialoguePanel.continueSpeechBtn.gameObject.SetActive(false);//生成选项时关闭继续按钮
            }

            
        } 
        else    
        {
            Debug.LogError("chooseButtonPre 为空");
        }
    }
    

    public void ChooseClickUpdateDialogueSpeech(int alpa)//点击时，更新对话  需要在生成按钮时就将按钮对应的List传入
    {
        
        currentDialogueNode = dialogueTree.dialogueNodeDic[alpa];
        dialoguePanel.continueSpeechBtn.gameObject.SetActive(true);

        ClearChoices();
    }

    public void SwitchPosition()//根据row的位置更换显示位置
    {
        if(dialoguePanel.rowImageLeft == null || dialoguePanel.rowImageRight == null || currentDialogueNode.dialogueList[currentSpeechCount].row != Row.Null)
        {
            dialoguePanel.rowImageRight.enabled = false;
            dialoguePanel.rowImageLeft.enabled = false;
        }
        
        switch (currentDialogueNode.dialogueList[currentSpeechCount].row)
        {
            case Row.left :
                dialoguePanel.rowImageLeft.gameObject.SetActive(true);
                dialoguePanel.rowImageRight.gameObject.SetActive(false);
                break;
            case Row.right :
                dialoguePanel.rowImageRight.gameObject.SetActive(true);
                dialoguePanel.rowImageLeft.gameObject.SetActive(false);
                break;
            case Row.Null :
                dialoguePanel.rowImageRight.gameObject.SetActive(false);
                dialoguePanel.rowImageLeft.gameObject.SetActive(false);
                break ;
        }
    }

    public void ClearChoices()// 销毁之前的选择按钮
    {
        ChooseButton[] chooseBtns = dialoguePanel.chooseParent.GetComponentsInChildren<ChooseButton>();
        foreach (ChooseButton choice in chooseBtns)
        {
            Destroy(choice.gameObject);
        }
    }

    public static void SetDialogueTree(DialogueTreeSO dialogueTree)//使用时调用此方法
    {
        Instance.dialogueTree = dialogueTree;
        Instance.currentDialogueNode = dialogueTree.dialogueNodeList[0];
        Instance.currentSpeechCount = 0;
    }
#region 存储数据本地化
    public void GetSaveData(Data data)
    {
        data.SerializableData(currentDialogueNode);
    }

    public void LoadData(Data data)
    {
        currentDialogueNode = data.DeSerializeData<DialogueNode>();
        SyncDialogueInformation(currentSpeechCount);//再次更新信息
    }
#endregion
}
public enum Row
{
    Null,left,right
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SaveButton : MonoBehaviour
{
    public Text saveText;
    public Toggle saveToggle;
    [Tooltip("生成按钮时传入的路径")]public string savePath;

    private void Awake() 
    {
       saveToggle.onValueChanged.AddListener(OnClickSaveToggle);
    }

    private void OnClickSaveToggle(bool isSelected)
    {
        if(isSelected)
        {
            saveToggle.isOn = true;

            SaveLoadManager.Instance.saveLoadPanel.loadButton.savePath = null;
            SaveLoadManager.Instance.saveLoadPanel.loadButton.savePath = savePath;

            SaveLoadManager.Instance.saveLoadPanel.deletButton.savePath = null;
            SaveLoadManager.Instance.saveLoadPanel.deletButton.savePath = savePath;    
        }
        Debug.Log("当前选择存档：" + saveText.text);
        
    }

    public SaveButtonData GetSaveButtonData()
    {
        return new SaveButtonData
        {
            saveName = this.saveText.text,
            savePath = this.savePath
        };
    }
}





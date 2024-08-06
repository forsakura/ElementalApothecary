using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadPanel : MonoBehaviour
{
    public Transform instanceSaveBtnPosParent;
    public SaveButton saveButtonPre;
    public LoadButton loadButton;
    public DeletButton deletButton;
    public List<SaveButtonData> saveButtonDataList = new List<SaveButtonData>();


    public void InstanceSaveBtn(string saveName,string savePath)
    {
        //SaveLoadManager.Instance.saveDataCatalogue.saveButtonDataList = saveButtonDataList;

        SaveButton saveBtn = Instantiate(saveButtonPre, instanceSaveBtnPosParent);

        saveBtn.saveText.text = saveName;
        saveBtn.savePath = savePath;
        saveBtn.saveToggle.group = instanceSaveBtnPosParent.GetComponent<ToggleGroup>();
        
        saveButtonDataList.Add(saveBtn.GetSaveButtonData());
    }
}




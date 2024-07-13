using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;
using Unity.VisualScripting;
[DefaultExecutionOrder(-100)]
public class SaveLoadManager : SingletonScript<SaveLoadManager>
{
    public SaveLoadPanel saveLoadPanel;
    private List<ISavable> savableList = new List<ISavable>();
    private Data saveData;
    public VoidEvent_SO saveEvent;
    public VoidEvent_SO loadEvent;
    public VoidEvent_SO clearSaves;
    public StringEvent_SO deletSave;
    public SaveDataCatalogue saveDataCatalogue;
    private string jsonFolder;
    protected override void Awake()
    {
        base.Awake();
        saveData = new Data();
        saveDataCatalogue = new SaveDataCatalogue();
    }

    private void Start()
    {
        jsonFolder = Application.persistentDataPath + SaveLoadPath.Save_Folder;

        ReadSavedData(GetLatestSavePath());
        LoadDataCatalogue(saveDataCatalogue);
    }

    private void OnEnable()
    {
        saveEvent.OneventRaised += Save;
        loadEvent.OneventRaised += Load;
        clearSaves.OneventRaised += DeleteAllSaves;
        deletSave.OneventRaised += Delete;
    }
    private void OnDisable()
    {
        saveEvent.OneventRaised -= Save;
        loadEvent.OneventRaised -= Load;
        clearSaves.OneventRaised -= DeleteAllSaves;
        deletSave.OneventRaised -= Delete;

        SaveCatalogue();
    }
    public static void RegisterSaveData(ISavable savableSave)
    {
        if (!Instance.savableList.Contains(savableSave))
        {
            Instance.savableList.Add(savableSave);
            Debug.Log("存档列表中的数据个数：" + Instance.savableList.Count);
        }
    }

    public static void UnRegisterSaveData(ISavable savable)
    {
        if (Instance.savableList.Contains(savable))
        {
            Instance.savableList.Remove(savable);
            Debug.Log("移除存档列表成功：" + savable.ToString());
        }
    }
    private static void SaveCatalogue()//Disable时生成存档目录
    {
        var resultPath = Instance.jsonFolder + SaveLoadPath.Save_Catalogue;
        var saveDataCatalogue = JsonConvert.SerializeObject(Instance.saveDataCatalogue);

        Directory.CreateDirectory(Instance.jsonFolder);
        File.WriteAllText(resultPath, saveDataCatalogue);

        Debug.Log("保存存档目录成功");
    }
    public static void Save()
    {
        try
        {
            foreach (var savable in Instance.savableList)
            {
                savable.GetSaveData(Instance.saveData);
            }


            System.DateTime time = System.DateTime.Now;

            var resultPath = Instance.jsonFolder + time.ToString("yyyy_MMdd_HHmmss") + SaveLoadPath.Save_File;
            Instance.saveLoadPanel.InstanceSaveBtn(time.ToString("yyyy_MMdd_HHmmss"), resultPath);
            var saveData = JsonConvert.SerializeObject(Instance.saveData);

            Directory.CreateDirectory(Instance.jsonFolder);
            File.WriteAllText(resultPath, saveData);

            Debug.Log("存档成功：" + resultPath);
        }
        catch (Exception ex)
        {
            Debug.LogError("存档失败" + ex.Message);
        }

    }

    public static void Load()
    {
        try
        {
            foreach (var savable in Instance.savableList)
            {
                savable.LoadData(Instance.saveData);
                Debug.Log("加载存档成功：" + savable.ToString());
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("加载失败" + ex.Message);
        }

    }

    private void LoadDataCatalogue(SaveDataCatalogue catalogueData)//启动时读取目录，点击对应SaveBtn时读取存档
    {
        var resultPath = Instance.jsonFolder + SaveLoadPath.Save_Catalogue;

        if (File.Exists(resultPath))
        {
            var stringcatalogueData = File.ReadAllText(resultPath);
            catalogueData = JsonConvert.DeserializeObject<SaveDataCatalogue>(stringcatalogueData);


            if (catalogueData != null)
            {
                saveLoadPanel.saveButtonDataList = catalogueData.saveButtonDataList;

                saveDataCatalogue.saveButtonDataList = catalogueData.saveButtonDataList;

                if (saveDataCatalogue.saveButtonDataList != null)//需要确保反序列化后的数据存在saveButtonDataList，否则会报空
                {
                    SaveButton saveButton = null;
                    for (int i = 0; i < saveDataCatalogue.saveButtonDataList.Count; i++)
                    {

                        saveButton = Instantiate(saveLoadPanel.saveButtonPre, saveLoadPanel.instanceSaveBtnPosParent);
                        //确保父级包含ToggleGroup
                        if (saveLoadPanel.instanceSaveBtnPosParent.GetComponent<ToggleGroup>() == null)
                        {
                            saveLoadPanel.instanceSaveBtnPosParent.AddComponent<ToggleGroup>();
                            saveButton.saveToggle.group = saveLoadPanel.instanceSaveBtnPosParent.GetComponent<ToggleGroup>();
                        }
                        else
                        {
                            saveButton.saveToggle.group = saveLoadPanel.instanceSaveBtnPosParent.GetComponent<ToggleGroup>();
                        }

                        saveButton.saveText.text = saveDataCatalogue.saveButtonDataList[i].saveName;
                        saveButton.savePath = saveDataCatalogue.saveButtonDataList[i].savePath;

                    }
                }

            }

            Debug.Log("读取存档目录成功" + resultPath);
        }



    }

    /// <summary>
    /// 删除指定路径的存档
    /// </summary>
    /// <param name="savePath">指定的路径</param>
    public static void Delete(string savePath)
    {
        var resultPath = savePath;

        if (File.Exists(resultPath))
        {
            File.Delete(resultPath);

            var itemsToRemove = new List<SaveButtonData>();
            foreach (var data in Instance.saveDataCatalogue.saveButtonDataList)
            {
                if (data.savePath == savePath)
                {
                    itemsToRemove.Add(data);
                }
            }

            foreach (var item in itemsToRemove)
            {
                Instance.saveDataCatalogue.saveButtonDataList.Remove(item);
                Instance.saveLoadPanel.saveButtonDataList.Remove(item);

                SaveButton[] saveButtons = Instance.saveLoadPanel.instanceSaveBtnPosParent.GetComponentsInChildren<SaveButton>();
                foreach (var btn in saveButtons)
                {
                    if (btn.savePath == savePath)
                        Destroy(btn.gameObject);
                }
            }
            Debug.Log("删除存档成功：" + resultPath);
        }
        else
        {
            Debug.Log("当前无存档：" + resultPath);
        }
    }

    /// <summary>
    /// 删除所有存档
    /// </summary>
    public static void DeleteAllSaves()
    {
        try
        {
            DirectoryInfo directory = new DirectoryInfo(Instance.jsonFolder);

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();

                Instance.saveLoadPanel.saveButtonDataList.Clear();
                Instance.saveDataCatalogue.saveButtonDataList.Clear();

                SaveButton[] saveButtons = Instance.saveLoadPanel.instanceSaveBtnPosParent.GetComponentsInChildren<SaveButton>();

                foreach (var item in saveButtons)
                {
                    Destroy(item.gameObject);
                }
            }

            Debug.Log("所有存档删除成功");
        }
        catch (Exception ex)
        {
            Debug.LogError("删除所有存档失败: " + ex.Message);
        }
    }
    /// <summary>
    /// 读取存档路径
    /// </summary>
    /// <param name="savePath"></param>
    public static void ReadSavedData(string savePath)
    {
        var resultPath = savePath;

        if (File.Exists(resultPath))
        {
            var stringData = File.ReadAllText(resultPath);
            Instance.saveData = JsonConvert.DeserializeObject<Data>(stringData);

            Debug.Log("读取存档成功" + resultPath);
        }
    }

    /// <summary>
    /// 找出文件夹中的最新存档，无存档时会找出目录
    /// </summary>
    /// <returns></returns>
    public static string GetLatestSavePath()
    {
        var directoryInfo = new DirectoryInfo(Instance.jsonFolder);
        if (!directoryInfo.Exists)
        {
            Debug.LogError("存档文件夹不存在：" + Instance.jsonFolder);
            return null;
        }

        FileInfo latestFile = null;
        foreach (var file in directoryInfo.GetFiles())
        {
            if (latestFile == null || file.LastWriteTime > latestFile.LastWriteTime)
            {
                latestFile = file;
            }
        }

        if (latestFile != null)
        {
            Debug.Log("找到最新的存档：" + latestFile.FullName);
            return latestFile.FullName;
        }
        else
        {
            Debug.Log("没有找到任何存档");
            return null;
        }
    }

    private void Update()
    {
        if (DialogueSystem.Instance != null)
            OpenPanel(KeyCode.T, DialogueSystem.Instance.dialoguePanel.gameObject);
        if (MessionSystem.Instance != null)
            OpenPanel(KeyCode.J, MessionSystem.Instance.messionPanel.gameObject);
        if (SaveLoadManager.Instance != null)
            OpenPanel(KeyCode.L, saveLoadPanel.gameObject);
    }

    private void OpenPanel(KeyCode key, GameObject panel)
    {
        if (Input.GetKeyDown(key))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }

}

public class SaveLoadPath
{
    public const string Save_Folder = "/SAVE DATA/";
    public const string Save_File = "data.save";
    public const string Save_Catalogue = "data.catalogue";
}

public enum PersistentType
{
    None, ReadWrite
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GMComand : MonoBehaviour
{
    [MenuItem("NuoYan/Easy Save/Test/Save")]
    public static void Save()
    {
        SaveLoadManager.Save();
    }
    [MenuItem("NuoYan/Easy Save/Test/Load")]
    public static void Load()
    {
        SaveLoadManager.Load();
    }
    [MenuItem("NuoYan/Easy Save/Test/DeletAll")]
    public static void DeleteAll()
    {
        SaveLoadManager.DeleteAllSaves();
    }

    [MenuItem("NuoYan/DialogueSystem/CreateDialoguePanel")]
    public static void CreateDialoguePanel()
    {
        var dialoguePanelObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DialogueSystem_nuoyan/Prefab/DialogueCanvas.prefab");
        if (dialoguePanelObj != null)
        {
            var dialoguePanel = dialoguePanelObj.GetComponent<DialoguePanel>();

            if (dialoguePanel != null)
            {
                var obj = Instantiate(dialoguePanel);

                CreateDialogueSystem(obj);

                Debug.Log("对话面板成功加载：" + obj.name);
            }
            else
            {
                Debug.LogError("未在目标Prefab上找到DialoguePanel");
            }
        }
        else
        {
            Debug.LogError("对话面板Prefab未成功加载。");
        }
    }
    public static void CreateDialogueSystem(DialoguePanel dialoguePanel)
    {
        var dialogueSystemObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DialogueSystem_nuoyan/Prefab/DialogueSystem.prefab");
        if (dialogueSystemObj != null)
        {
            var dialogueSystem = dialogueSystemObj.GetComponent<DialogueSystem>();

            if (dialogueSystemObj != null)
            {
                var obj = Instantiate(dialogueSystem);
                obj.GetComponent<DialogueSystem>().dialoguePanel = dialoguePanel;
                Debug.Log("对话系统成功加载：" + obj.name);
            }
            else
            {
                Debug.LogError("未在目标Prefab上找到DialogueSystem");
            }
        }
        else
        {
            Debug.LogError("对话面板Prefab未成功加载。");
        }
    }
    [MenuItem("NuoYan/Easy Save/SaveLoadPanel")]
    public static void CreateSaveLoadPanel()
    {
        var saveLoadPanelObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DialogueSystem_nuoyan/Prefab/SaveLoadPanel.prefab");
        if (saveLoadPanelObj != null)
        {
            var saveLoadPanel = saveLoadPanelObj.GetComponent<SaveLoadPanel>();

            if (saveLoadPanel != null)
            {
                var obj = Instantiate(saveLoadPanel);

                CreateSaveLoadManager(obj);
            }


        }
    }
    public static void CreateSaveLoadManager(SaveLoadPanel saveLoadPanel)
    {
        var saveLoadObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DialogueSystem_nuoyan/Prefab/SaveLoadManager.prefab");
        if (saveLoadObj != null)
        {
            var saveLoadManager = saveLoadObj.GetComponent<SaveLoadManager>();

            if (saveLoadManager != null)
            {
                var obj = Instantiate(saveLoadObj);

                obj.GetComponent<SaveLoadManager>().saveLoadPanel = saveLoadPanel;//对生成后赋值
                Debug.Log("存储系统成功加载：" + obj.name);
            }
            else
            {
                Debug.LogError("未在目标Prefab上找到SaveLoadManager");
            }
        }
        else
        {
            Debug.LogError("SaveLoadManager未成功加载。");
        }
    }
    [MenuItem("NuoYan/MessionSystem/CreateMessionPanel")]
    public static void CreateMessionPanel()
    {
        var messionPanelObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DialogueSystem_nuoyan/Prefab/TaskCanvas.prefab");
        if (messionPanelObj != null)
        {
            var messionPanel = messionPanelObj.GetComponent<MessionPanel>();

            if (messionPanel != null)
            {
                var obj = Instantiate(messionPanel);

                CreateMessionSystem(obj);

                Debug.Log("存储系统成功加载：" + obj.name);
            }
            else
            {
                Debug.LogError("未在目标Prefab上找到messionPanel");
            }
        }
        else
        {
            Debug.LogError("messionPanel未成功加载。");
        }
    }
    public static void CreateMessionSystem(MessionPanel messionPanel)
    {
        var messionSysObj = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/DialogueSystem_nuoyan/Prefab/MessionSystem.prefab");
        if (messionSysObj != null)
        {
            var messionSystem = messionSysObj.GetComponent<MessionSystem>();

            if (messionSysObj != null)
            {
                var obj = Instantiate(messionSystem);
                obj.GetComponent<MessionSystem>().messionPanel = messionPanel;
                Debug.Log("任务系统成功加载：" + obj.name);
            }
            else
            {
                Debug.LogError("未在目标Prefab上找到MessionSystem");
            }
        }
        else
        {
            Debug.LogError("任务系统Prefab未成功加载。");
        }
    }
}

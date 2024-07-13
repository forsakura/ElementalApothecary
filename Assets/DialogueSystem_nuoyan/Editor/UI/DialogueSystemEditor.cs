using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueSystemEditor : EditorWindow
{
    public DialogueTreeView dialogueTreeView;
    public InspectorView inspectorView;

    public Button updateBtn;

    [MenuItem("NuoYan/DialogueSystem/DialogueSystemEditor")]
    public static void ShowExample()
    {
        DialogueSystemEditor wnd = GetWindow<DialogueSystemEditor>();
        wnd.titleContent = new GUIContent("DialogueSystemEditor");
    }

    public void CreateGUI()
    {
         // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;


        // Instantiate UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/DialogueSystem_nuoyan/Editor/UI/DialogueSystemEditor.uxml");
        visualTree.CloneTree(root);

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DialogueSystem_nuoyan/Editor/UI/DialogueSystemEditor.uss");
        root.styleSheets.Add(styleSheet);

        dialogueTreeView = root.Q<DialogueTreeView>();
        inspectorView = root.Q<InspectorView>();

        dialogueTreeView.OnDialogueSelected = OnDialogueSelectedChange;//当选择改变时，同步更新Inspector面板内的信息

        updateBtn = root.Q<Button>("update");

        updateBtn.clicked += UpdateBtnClicked;
   
    }

    

    private void UpdateBtnClicked()
    {
        foreach (var item in dialogueTreeView.dialogueTree.dialogueNodeList)
        {
            item.name = item.ID.ToString();
            dialogueTreeView.dialogueTree.ReName();
            Debug.Log("更新成功");
        }
        
        
    }

    private void OnDialogueSelectedChange(DialogueView view)
    {
        inspectorView.UpdateSelect(view);
    }

    private void OnSelectionChange() 
    {
        DialogueTreeSO dialogueTree = Selection.activeObject as DialogueTreeSO;
        if(dialogueTree)
            dialogueTreeView.PopulateDialogueTree(dialogueTree);
    }
}

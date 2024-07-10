using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InspectorView : VisualElement
{
    public class UxmlFactroy : UxmlFactory<InspectorView,UxmlTraits>{}
    Editor editor;
    internal void UpdateSelect(DialogueView dialogueView)
    {
        
        Clear();//清除之前的节点信息
        Object.DestroyImmediate(editor);
        editor = Editor.CreateEditor(dialogueView.dialogueNode);

        IMGUIContainer container = new IMGUIContainer(() => 
        {
            if(editor.target)
            {
                editor.OnInspectorGUI();
            }
        });
        Add(container);

    }
}

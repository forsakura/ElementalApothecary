using UnityEngine;
using UnityEngine.UIElements;

namespace _3rd.DialogueSystem_nuoyan.Editor.Script
{
    public class InspectorView : VisualElement
    {
        public class UxmlFactroy : UxmlFactory<InspectorView,UxmlTraits>{}
        UnityEditor.Editor editor;
        internal void UpdateSelect(DialogueView dialogueView)
        {
        
            Clear();//清除之前的节点信息
            Object.DestroyImmediate(editor);
            editor = UnityEditor.Editor.CreateEditor(dialogueView.dialogueNode);

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
}

using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(Element))] 
    public class ElementEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();


            if (GUILayout.Button("ReSet"))
            {
                Element ele = (Element)target; 
                ele.ReSetElement();
            }
        }
    }
}

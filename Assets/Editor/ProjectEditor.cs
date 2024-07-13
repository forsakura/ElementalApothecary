using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Element))] 
public class ElementEditor : Editor
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

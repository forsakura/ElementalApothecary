using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ATTRSO", menuName = "ScriptableObjects/ATTRSO")]
public class ATTRSO : ScriptableObject
{
    public List<BaseAttribute> attributes = new List<BaseAttribute>();
    public bool keepUpdating;

    private Dictionary<int, BaseAttribute> attributesById = new Dictionary<int, BaseAttribute>();

    public List<BaseAttribute> Attributes => attributes;

    public BaseAttribute GetAttributeById(int id)
    {
        attributesById.TryGetValue(id, out var attribute);
        return attribute;
    }

    private void OnEnable()
    {
        InitializeDictionaries();
    }

    private void OnValidate()
    {
        if(keepUpdating)
        InitializeDictionaries();
    }

    public void InitializeDictionaries()
    {
        attributesById.Clear();

        foreach (var attribute in attributes)
        {
            if (attribute != null)
            {
                attributesById[attribute.id] = attribute;
            }
        }
    }
}

[Serializable]
public abstract class BaseAttribute : ScriptableObject
{
    public string Name;
    public int id;
    public List<int> effectId;//涉及的效果id(意思说属性可以组合)
    public List<int> buffId;//涉及的buffid
    public string description;
    public AttributeType Type;
    public EElement baseElement;
    public float Duration;
    public float _remainingTime;
    public float leak = 0;
    public bool IsPermanent= true;

    public enum AttributeType
    { 
        Main,
        Aux
    }



    //这些GameObject估计要改
    public virtual void OnApply(GameObject target) { }
    public virtual void OnExpired(GameObject target) { }
    public virtual void OnUpdate(GameObject target, float deltaTime) { }
    public virtual bool IsExpired()
    {
        return !IsPermanent && _remainingTime <= 0;
    }
}

[CustomEditor(typeof(ATTRSO))]
public class ATTRSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ATTRSO attrSO = (ATTRSO)target;

        if (GUILayout.Button("Update Dictionaries"))
        {
            attrSO.InitializeDictionaries();
            EditorUtility.SetDirty(attrSO); // 标记对象为已修改  
        }
    }
}

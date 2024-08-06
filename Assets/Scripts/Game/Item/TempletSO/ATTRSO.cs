using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ATTRSO", menuName = "ScriptableObjects/ATTRSO")]
public class ATTRSO : ScriptableObject
{
    public List<Attribute> attributes;
    public bool keepUpdating;

    private Dictionary<int, Attribute> attributesById = new Dictionary<int, Attribute>();

    public List<Attribute> Attributes => attributes;

    public Attribute GetAttributeById(int id)
    {
        attributesById.TryGetValue(id, out var attribute);
        return attribute;
    }

    public List<Attribute> GetAttributesByElement(EElement element)
    {
        return attributes.Where(attribute => attribute.baseElement == element).ToList();
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
public  class Attribute:ScriptableObject
{
    public int id;
    public EElement baseElement;
    public float Duration;
    public float _remainingTime;
    public bool IsPermanent=true;

    public virtual void Apply(GameObject target) { }
    public virtual void Update(GameObject target, float deltaTime) { }
    public virtual void Expire(GameObject target) { }
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

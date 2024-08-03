using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TrItem;

[CreateAssetMenu(fileName = "TempletSO", menuName = "ScriptableObjects/TempletSO")]
public class TempletSO : ScriptableObject
{
    [SerializeField]
    private List<ItemTemplet> itemTemplets;

    public List<ItemTemplet> ItemTemplets => itemTemplets;

    // 根据 ID 查找 ItemTemplet  
    public ItemTemplet GetItemTempletById(int id)
    {
        return itemTemplets.FirstOrDefault(item => item.id == id);
    }

    // 根据元素类型查找 ItemTemplet  
    public List<ItemTemplet> GetItemTempletsByElement(EElement element)
    {
        return itemTemplets.Where(item => item.BaseElement == element).ToList();
    }
}

[Serializable]
public class ItemTemplet
{
    public int id;
    public int itemUseRadius;
    public string itemDescription;
    public Sprite itemIcon; // 这里存路径也可以，看需求  
    public string itemName;
    public List<ItemTag> tags;
    public EElement BaseElement;
    public Vector2 currentElementCount; 
}



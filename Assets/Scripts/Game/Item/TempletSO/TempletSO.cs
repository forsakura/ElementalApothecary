using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TrItem;

[CreateAssetMenu(fileName = "TempletSO", menuName = "ScriptableObjects/TempletSO")]
public class TempletSO : ScriptableObject
{
    List<ItemTemplet> ItemTemplets;
}

/// <summary>
/// 存初始数据与初始随机效果数据
/// </summary>
public class ItemTemplet
{
    int id;
    public int itemUseRadius;
    public string itemDescription;
    //public Sprite itemIcon;
    public string itemName;
    public List<ItemTag> tags;//这个没有很细的去想，先这么做
    public EElement BaseElement;
    public Vector2 currentElementCount;
}



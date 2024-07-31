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
/// ���ʼ�������ʼ���Ч������
/// </summary>
public class ItemTemplet
{
    int id;
    public int itemUseRadius;
    public string itemDescription;
    //public Sprite itemIcon;
    public string itemName;
    public List<ItemTag> tags;//���û�к�ϸ��ȥ�룬����ô��
    public EElement BaseElement;
    public Vector2 currentElementCount;
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʒ���ݣ���������ͨ��id��SO���ù�������
/// </summary>
public class DataItem : TrItem
{
    public int itemUseRadius;
    public string itemDescription; 
    //public Sprite itemIcon; 
    public string itemName;
    public List<ItemTag> tags;//���û�к�ϸ��ȥ�룬����ô��
    public EElement BaseElement;
    public Vector2 currentElementCount;

    /// <summary>
    /// ��ʼ������ģ��
    /// </summary>
    public void initByTemplet()
    {

    }

    public void applyATTR(int id)
    {

    }
}

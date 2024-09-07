using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʒ���ݣ���������ͨ��id��SO���ù�������
/// </summary>
public class DataItem : TrItem, IDataItem
{
    private int _useTimes;
    private int _itemUseRadius;
    private string _itemDescription;
    private Sprite _itemIcon; // �����Ҫ������ȡ��ע�Ͳ������Ӧ������  
    private string _itemName;
    private List<TrItem.ItemTag> _tags = new List<TrItem.ItemTag>();
    private EElement _baseElement ;
    private float _currentElementCount;

    public DataItem()
    {
        this.useTimes = 0;
        this.itemUseRadius = 0;
        this.itemDescription = null;
        this.itemIcon = null;
        this.itemName = null;
        this.tags = null;
        BaseElement = EElement.None;
        this.currentElementCount = 0;
    }

    public int useTimes
    {
        get => _useTimes;
        set => _useTimes = value;
    }

    public int itemUseRadius
    {
        get => _itemUseRadius;
        set => _itemUseRadius = value;
    }

    public string itemDescription
    {
        get => _itemDescription;
        set => _itemDescription = value;
    }

    public Sprite itemIcon
    {
        get => _itemIcon;
        set => _itemIcon = value;
    }

    public string itemName
    {
        get => _itemName;
        set => _itemName = value;
    }

    public List<TrItem.ItemTag> tags
    {
        get => _tags;
        set => _tags = value;
    }

    public EElement BaseElement
    {
        get => _baseElement;
        set => _baseElement = value;
    }

    public float currentElementCount
    {
        get => _currentElementCount;
        set => _currentElementCount = value;
    }

    /// <summary>
    /// ��ʼ������ģ��
    /// </summary>
    public void initByTemplet()
    {

    }

    public void applyATTR()
    {
        
    }

    /// <summary>  
    /// ����������ݵ�����̨���ڵ���  
    /// </summary>  
    public void DebugDisplayData()
    {
        string tagList = string.Join(", ", tags);
        string baseElementList = string.Join(", ", BaseElement);

        string debugInfo = $"DataItem Info:\n" +
                           $"ID: {ID.id}\n" +  
                           $"Base ID: {ID.BaseId}\n" +  
                           $"Attributes: {string.Join(", ", ID.ATTR)}\n"+ 
                           $"Item Name: {itemName}\n" +
                           $"Use Times: {useTimes}\n" +
                           $"Item Use Radius: {itemUseRadius}\n" +
                           $"Item Description: {itemDescription}\n" +
                           $"Tags: {tagList}\n" +
                           $"Base Elements: {baseElementList}\n" +
                           $"Current Element Count: {currentElementCount}";

        Debug.Log(debugInfo);
    }
}

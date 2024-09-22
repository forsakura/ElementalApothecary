using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʒ���ݣ���������ͨ��id��SO���ù�������
/// </summary> 
[System.Serializable]
public class DataItem : TrItem, IDataItem
{
    [SerializeField]
    private int _useTimes;
    [SerializeField]
    private int _itemUseRadius;
    [SerializeField]
    private string _itemDescription;
    [SerializeField]
    private Sprite _itemIcon;
    [SerializeField]
    private string _itemName;
    [SerializeField]
    private List<TrItem.ItemTag> _tags = new List<TrItem.ItemTag>();
    [SerializeField]
    private EElement _baseElement;
    [SerializeField]
    private float _currentElementCount;

    public DataItem()
    {
        this.useTimes = 0;
        this.itemUseRadius = 0;
        this.itemDescription = "";
        this.itemIcon = null;
        this.itemName = "";
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

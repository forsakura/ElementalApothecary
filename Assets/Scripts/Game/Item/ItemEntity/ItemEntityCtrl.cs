using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntityCtrl : MonoBehaviour
{
    [SerializeField]
    SOListForItemEntity sOListForItemEntity;
    [SerializeField]
    DataItem data;
    [SerializeField]
    IATTRManager AttributeManager;
    [SerializeField]
    BulletControl bulletComponent;
    

    public DataItem Data { get => data; set => data = value; }
    public BulletControl BulletComponent { get => bulletComponent; set => bulletComponent = value; }

    private void Awake()
    {
        AttributeManager = GetComponent<ATTRManager>();
        bulletComponent = GetComponent<BulletControl>();
    }

    void initATTR()
    {
        foreach (var attr in data.ID.ATTR)
        {
            AttributeManager.Add(
                                sOListForItemEntity.AttributeSO.GetAttributeById(attr)
                                );
        }
    }

}

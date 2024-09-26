using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntityCtrl : MonoBehaviour
{
    [SerializeField]
    DataItem data;
    [SerializeField]
    IATTRManager AttributeManager;
    [SerializeField]
    BulletControl BulletComponent;

    public DataItem Data { get => data; set => data = value; }

    private void Awake()
    {
        AttributeManager = GetComponent<ATTRManager>();
        BulletComponent = GetComponent<BulletControl>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntityCtrl : MonoBehaviour
{
    DataItem Data;
    ATTRManager AttributeManager;
    BulletControl BulletComponent;

    private void Awake()
    {
        AttributeManager = GetComponent<ATTRManager>();
        BulletComponent = GetComponent<BulletControl>();
    }
}

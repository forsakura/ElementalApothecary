using ProjectBase.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FlaskPanel : BasePanel
{
    public SlotUI[] falskSlots;//ǰ����Ϊ�زģ�������Ϊ����͸�����
    public InputField falskInputs;//���Ƶ�����

    protected override void Awake()
    {
        base.Awake();
        GetControl<Button>("QuitBtn").onClick.AddListener(() =>
        {
            
            for(int i=0;i<InventoryManager.Instance.flaskBag.itemList.Count;i++)
            {
                if (InventoryManager.Instance.flaskBag.itemList[i].itemID != null)
                {
                    InventoryManager.Instance.boxBag.itemList.Add(InventoryManager.Instance.flaskBag.itemList[i]);
                    InventoryManager.Instance.flaskBag.itemList[i]= new InventoryItem();
                }
            }
            foreach(var slot in falskSlots)
            {
                slot.UpdateEmptySlot();
            }
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, InventoryManager.Instance.boxBag.itemList);

            UIManager.Instance.HidePanel("FlaskPanel");
        });
        GetControl<Button>("SureBtn").onClick.AddListener(() =>
        {
            //�ϳɹ���
        });
    }

}

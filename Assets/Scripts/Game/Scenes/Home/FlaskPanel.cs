using ProjectBase.UI;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FlaskPanel : BasePanel
{
    public SlotUI[] falskSlots;//ǰ����Ϊ�زģ�������Ϊ����͸�����
    public InputField falskInputs;//���Ƶ�����

    ISynthesis synthesis;

    protected override void Awake()
    {
        base.Awake();
        synthesis = GetComponent<Synthesis>();

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
            for(int i=0;i < synthesis.MaxMaterialEnum; i++)
            {
                if (falskSlots[i].itemDetails != null)
                {           
                    synthesis.addMaterial(i, falskSlots[i].itemDetails);
                }    
            }

            DataItem output = synthesis.output();
            if (output != null)
            {
                output.DebugDisplayData();
                falskSlots[synthesis.MaxMaterialEnum].UpdateSlot(output, 1);
            
                for (int i = 0; i < synthesis.MaxMaterialEnum; i++)
                {
                    falskSlots[i].UpdateEmptySlot();
                }
            }
            
            synthesis.init();
            //�ϳɹ���
        });
    }

}

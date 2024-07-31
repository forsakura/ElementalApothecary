using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotPanel : BasePanel<PotPanel>
{
    public Button sureBtn;
    public Button functionBtn;
    public InputField potionNum;
    public SlotUI input1;
    public SlotUI input2;
    public SlotUI output;
    public override void Init()
    {
        sureBtn.onClick.AddListener(() =>
        {
            Refining();
            //FunctionTablePanel.Instance.AddFunction(input1.itemDetails.itemID, input2.itemDetails.itemID);
        });
        functionBtn.onClick.AddListener(() =>
        {
            FunctionTablePanel.Instance.ShowMe();
            HideMe();
        });
        HideMe();
    }
    private void Update()
    {
        if (input1.itemAmount>0&&input2.itemAmount>0)
        {
            //output.itemDetails=InventoryManager.Instance.GetItemDetails(InventoryManager.Instance.ReturnProductionID(input1.itemDetails.itemID,input2.itemDetails.itemID));
            output.SetOutputMod();
        }
    }

    public void Refining()
    {
        int refineNum = int.Parse(potionNum.text);
        if (refineNum > 0)
        {
            if(input1.itemAmount>=refineNum&&input2.itemAmount>=refineNum)
            {
                var items = InventoryManager.Instance.potBag.itemList;
                int amount1 = items[0].itemAmount - refineNum;
                int amount2 = items[1].itemAmount - refineNum;
                InventoryManager.Instance.potBag.itemList[0]=new LegacyInventoryItem { itemID = items[0].itemID,itemAmount=amount1 };
                InventoryManager.Instance.potBag.itemList[1] = new LegacyInventoryItem { itemID = items[1].itemID, itemAmount = amount2 };

                InventoryManager.Instance.AddItem(output.itemDetails.ID, refineNum*10);
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, InventoryManager.Instance.boxBag.itemList);
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Pot, InventoryManager.Instance.potBag.itemList);
            }
        }
    }
}

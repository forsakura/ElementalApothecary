using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

public class DistillerPanel : BasePanel<DistillerPanel>
{
    public Button foreverEffect;
    public Button sureBtn;
    public InputField potionNum;
    public SlotUI input1;
    public SlotUI input2;
    public SlotUI output;
    public override void Init()
    {
        foreverEffect.onClick.AddListener(() =>
        {
            ForeverEffectTable.Instance.ShowMe();
            HideMe();
        });
        sureBtn.onClick.AddListener(() =>
        {
            Distillering();
        });
        HideMe();
    }
    private void Update()
    {
        if (input1.itemAmount > 0 && input2.itemAmount > 0)
        {
            output.itemDetails = Distillation(input1.itemDetails, input2.itemDetails);
            output.SetOutputMod();
        }
    }

    public LegacyItemDetails Distillation(LegacyItemDetails potion1, LegacyItemDetails potion2)
    {
        var newPotion = new LegacyItemDetails();
        var newPurity = (potion1.purity + potion1.purity + 2) / 4;
        if (newPurity > 0.99) newPurity = 1;

        int purityNum = (int)(newPurity * 100);
        int newitemID=int.Parse(potion1.itemID.ToString().Substring(0,3)+(purityNum*100).ToString());
        LegacyItemDetails newItem=new LegacyItemDetails();
        foreach (var VARIABLE in InventoryManager.Instance.itemDataList_SO.itemDetailsList)
        {
            if (int.Parse(potion1.itemID.ToString().Substring(0, 3)) == VARIABLE.itemID)
            {

                newItem.itemID = newitemID;
                newItem.itemDescription = VARIABLE.itemDescription;
                newItem.itemIcon = VARIABLE.itemIcon;
                newItem.itemName = VARIABLE.itemName;
                newItem.itemType = VARIABLE.itemType;
                newItem.itemUseRadius = VARIABLE.itemUseRadius;
                newItem.itemOnWorldSprite = VARIABLE.itemOnWorldSprite;
                newItem.foeverEffect = VARIABLE.foeverEffect;
                newItem.effectsIDs = VARIABLE.effectsIDs;
                newItem.purity = purityNum;
            }
            if (VARIABLE.itemID == newitemID)
            {
                return VARIABLE;
            }
        }
        InventoryManager.Instance.itemDataList_SO.itemDetailsList.Add(newItem);
        return newPotion;
    }

    public void Distillering()
    {
        int refineNum = int.Parse(potionNum.text);
        if (refineNum > 0)
        {
            if (input1.itemAmount >= refineNum && input2.itemAmount >= refineNum)
            {
                var items = InventoryManager.Instance.distillerBag.itemList;
                int amount1 = items[0].itemAmount - refineNum;
                int amount2 = items[1].itemAmount - refineNum;
                InventoryManager.Instance.distillerBag.itemList[0] = new LegacyInventoryItem { itemID = items[0].itemID, itemAmount = amount1 };
                InventoryManager.Instance.distillerBag.itemList[1] = new LegacyInventoryItem { itemID = items[1].itemID, itemAmount = amount2 };

                InventoryManager.Instance.AddItem(output.itemDetails.itemID, refineNum);
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, InventoryManager.Instance.boxBag.itemList);
                EventHandler.CallUpdateInventoryUI(InventoryLocation.Pot, InventoryManager.Instance.distillerBag.itemList);
            }
        }
    }
}

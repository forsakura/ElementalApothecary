using ProjectBase.UI;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : BasePanel
{
    public SlotUI[] bagSlots;
    protected override void Awake()
    {
        base.Awake();
        GetControl<Button>("PutInBtn").onClick.AddListener(() =>
        {
            //将背包物品全部存入仓库
            PutAllItemIntoBox();
        });
        
        GetControl<Button>("CloseBtn").onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel("BagPanel");
        });
    }

    public void PutAllItemIntoBox()
    {
        for (int i = 0; i < InventoryManager.Instance.playerBag.itemList.Count; i++)
        {
            if (InventoryManager.Instance.playerBag.itemList[i].itemID != null)
            {
                InventoryManager.Instance.boxBag.itemList.Add(InventoryManager.Instance.playerBag.itemList[i]);
                InventoryManager.Instance.playerBag.itemList[i] = new InventoryItem();
            }
        }
        foreach (var slot in bagSlots)
        {
            slot.UpdateEmptySlot();
        }
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, InventoryManager.Instance.boxBag.itemList);

        UIManager.Instance.HidePanel("BagPanel");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxPannel : BasePanel<BoxPannel>
{
    public Button materialBtn;
    public Button lotionBtn;
    public Button allBtn;
    public InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();
    public override void Init()
    {
        materialBtn.onClick.AddListener(() =>
        {
            ShowSlot(ItemType.Material);
        });
        lotionBtn.onClick.AddListener(() =>
        {
            ShowSlot(ItemType.Potion);
        });
        allBtn.onClick.AddListener(() =>
        {
            foreach (SlotUI slot in inventoryUI.boxSlots)
            {
                slot.gameObject.SetActive(true);
            }
        });
        this.gameObject.SetActive(false);
    }

    public void ShowSlot(ItemType itemType)
    {
        foreach(SlotUI slot in inventoryUI.boxSlots)
        {
            if (slot.itemDetails != null)
            {
                if (slot.itemDetails.itemType == itemType)
                {
                    slot.gameObject.SetActive(true);
                }
                else
                {
                    slot.gameObject.SetActive(false);
                }
            }
            
        }
    }


}

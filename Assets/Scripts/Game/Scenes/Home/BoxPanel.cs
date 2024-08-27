using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ProjectBase.UI;

public class BoxPanel : BasePanel
{
    //public Button materialBtn;
    //public Button lotionBtn;
    //public Button allBtn;
    public InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();
    protected override void Awake()
    {
        base.Awake();
        GetControl<Button>("MaterialBtn").onClick.AddListener(() =>
        {
            ShowSlot(ItemType.Material);
        });
        GetControl<Button>("LotionBtn").onClick.AddListener(() =>
        {
            ShowSlot(ItemType.Potion);
        });
        GetControl<Button>("AllBtn").onClick.AddListener(() =>
        {
            foreach (SlotUI slot in inventoryUI.boxSlots)
            {
                slot.gameObject.SetActive(true);
            }
        });
        GetControl<Button>("CloseBtn").onClick.AddListener(() => 
        {
            UIManager.Instance.HidePanel("BoxPanel");
        });
    }

    

    //public override void Init()
    //{
    //    materialBtn.onClick.AddListener(() =>
    //    {
    //        ShowSlot(ItemType.Material);
    //    });
    //    lotionBtn.onClick.AddListener(() =>
    //    {
    //        ShowSlot(ItemType.Potion);
    //    });
    //    allBtn.onClick.AddListener(() =>
    //    {
    //        foreach (SlotUI slot in inventoryUI.boxSlots)
    //        {
    //            slot.gameObject.SetActive(true);
    //        }
    //    });
    //    this.gameObject.SetActive(false);
    //}
    /// <summary>
    /// 显示特定类型的物品，待修改
    /// </summary>
    /// <param name="itemType"></param>
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

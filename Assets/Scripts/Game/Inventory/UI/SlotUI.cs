using JetBrains.Annotations;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("组件获取")]
    [SerializeField] private Image slotImage;
    [SerializeField] private TextMeshProUGUI amountText;
    public Image slotHightlight;
    [SerializeField] private Button button;

    [Header("格子类型")]
    public ContainerType slotType;

    public bool isSelected;
    public int slotIndex;

    //物品信息
    public LegacyItemDetails itemDetails;
    public int itemAmount;

    public InventoryLocation Location
    {
        get
        {
            return slotType switch
            {
                ContainerType.Bag => InventoryLocation.Bag,
                ContainerType.Box => InventoryLocation.Box,
                ContainerType.Pot => InventoryLocation.Pot,
                ContainerType.Distiller => InventoryLocation.Distiller,
                _ => InventoryLocation.Bag
            };
        }
    }

    public InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

    private void Start()
    {
        isSelected = false;
        if (itemDetails == null)
        {
            UpdateEmptySlot();
        }
    }

    /// <summary>
    /// 更新格子UI和信息
    /// </summary>
    /// <param name="item">ItmDetails</param>
    /// <param name="amount">持有数量</param>
    public void UpdateSlot(LegacyItemDetails item, int amount)
    {
        itemDetails = item;
        //slotImage.sprite = item.itemIcon;
        itemAmount = amount;
        amountText.text = amount.ToString();
        slotImage.enabled = true;
        button.interactable = true;
    }
    /// <summary>
    /// 将Slot更新为空
    /// </summary>
    public void UpdateEmptySlot()
    {
        if (isSelected)
        {
            isSelected = false;
            inventoryUI.UpdateSlotHightlight(-1);
            EventHandler.CallItemSelectedEvent(itemDetails, isSelected);
        }
        itemDetails = null;
        slotImage.enabled = false;
        itemAmount = 0;
        amountText.text = string.Empty;
        button.interactable = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemDetails == null) return;
        isSelected = !isSelected;

        inventoryUI.UpdateSlotHightlight(slotIndex);

        if (slotType == ContainerType.Bag)
        {
            //通知物品被选中的状态和信息
        EventHandler.CallItemSelectedEvent(itemDetails, isSelected);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (itemAmount>0)
        {
            
            inventoryUI.dragItem.enabled = true;
            inventoryUI.dragItem.sprite = slotImage.sprite;

            isSelected = true;
            inventoryUI.UpdateSlotHightlight(slotIndex);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        Vector3 mouseScreenPos = Input.mousePosition;
        //mouseScreenPos.z = 10;
        //Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        inventoryUI.dragItem.preserveAspect = true;
        inventoryUI.dragItem.transform.position = mouseScreenPos;
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inventoryUI.dragItem.enabled = false;

        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            
            if (eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>() == null)
                return;
            SlotUI targetSlot = eventData.pointerCurrentRaycast.gameObject.GetComponent<SlotUI>();
            
            int targetIndex = targetSlot.slotIndex;
            if (slotType == targetSlot.slotType && slotIndex == targetIndex) return;
            //在Player自身背包范围内交换
            if (slotType == ContainerType.Bag && targetSlot.slotType == ContainerType.Bag)
            {
                InventoryManager.Instance.SwapItem(slotIndex, targetIndex);
            }
            else if (slotType == ContainerType.Box && targetSlot.slotType == ContainerType.Box)
            {
                InventoryManager.Instance.SwapItem(InventoryLocation.Box,slotIndex,InventoryLocation.Box, targetIndex);
            }
            else if (slotType == ContainerType.Box && targetSlot.slotType == ContainerType.Bag)
            {
                InventoryManager.Instance.SwapItem(InventoryLocation.Box, slotIndex, InventoryLocation.Bag, targetIndex);
            }
            else if (slotType == ContainerType.Bag && targetSlot.slotType == ContainerType.Box) 
            {
                InventoryManager.Instance.SwapItem(InventoryLocation.Bag, slotIndex, InventoryLocation.Box, targetIndex);
            }
            else if(slotType == ContainerType.Box && targetSlot.slotType == ContainerType.Pot)
            {
                //if(itemDetails.itemType != ItemType.Potion && itemDetails.itemID!=301)
                InventoryManager.Instance.SwapItem(InventoryLocation.Box, slotIndex, InventoryLocation.Pot, targetIndex);
            }
            else if (slotType == ContainerType.Pot && targetSlot.slotType == ContainerType.Box)
            {
                InventoryManager.Instance.SwapItem(InventoryLocation.Pot, slotIndex, InventoryLocation.Box, targetIndex);
            }
            else if (slotType == ContainerType.Box && targetSlot.slotType == ContainerType.Distiller)
            {

                if (itemDetails.itemType == ItemType.Potion)
                InventoryManager.Instance.SwapItem(InventoryLocation.Box, slotIndex, InventoryLocation.Distiller, targetIndex);
            }
            else if (slotType == ContainerType.Distiller && targetSlot.slotType == ContainerType.Box)
            {
                InventoryManager.Instance.SwapItem(InventoryLocation.Distiller, slotIndex, InventoryLocation.Box, targetIndex);
            }

            //清空所有高亮显示
            inventoryUI.UpdateSlotHightlight(-1);
        }
    }

    public void SetOutputMod()
    {
        slotImage.enabled = true;
        slotImage.sprite = itemDetails.itemIcon;

        button.interactable = false;
        amountText.enabled = false;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


    [RequireComponent(typeof(SlotUI))]
    public class ShowItemToolTip : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        private SlotUI slotUI;
        private InventoryUI inventoryUI => GetComponentInParent<InventoryUI>();

        private void Awake()
        {
            slotUI = GetComponent<SlotUI>();
        }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (slotUI.itemAmount > 0 )
        {
            inventoryUI.itemToolTip?.gameObject.SetActive(true);
            inventoryUI.itemToolTip?.SetupTooltip(slotUI.itemDetails, slotUI.slotType);
            inventoryUI.itemToolTip.GetComponent<RectTransform>().pivot = new Vector2(0f, 1);
            inventoryUI.itemToolTip.transform.position = transform.position + Vector3.right*50f;
        }

        else
        {
            inventoryUI.itemToolTip.gameObject?.SetActive(false);
        }
    }

        public void OnPointerExit(PointerEventData eventData)
        {
            inventoryUI.itemToolTip.gameObject.SetActive(false);
        }
    }
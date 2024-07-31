using ProjectBase.Res;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    public ItemToolTip itemToolTip;
    
    public ItemDataList_SO itemList;

    public Transform content;

    [Header("拖拽图片")]
    public Image dragItem;

    [Header("玩家背包")]
    [SerializeField] private GameObject bagUI;
    private bool bagOpened;

    [Header("仓库")]
    [SerializeField] private GameObject baseBag;
    private bool boxOpened;

    //[Header("交换UI")]
    //public ExchangeUI tradeUI;
    //public TextMeshProUGUI playerMoneyText;

    public List<SlotUI> boxSlots;
    [SerializeField] private SlotUI[] playerBagSlots;

    [Header("各类容器")]
    public SlotUI[] falskSlots;
    public SlotUI[] potSlots;
    public SlotUI[] distllerSlots;
    public SlotUI[] furnaceSlots;
    private void OnEnable()
    {
        EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadedEvent;
        EventHandler.BaseBagOpenEvent += OnBaseBagOpenEvent;
        EventHandler.BaseBagCloseEvent += OnBaseBagCloseEvent;
    }

    private void OnDisable()
    {
        EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadedEvent;
        EventHandler.BaseBagOpenEvent -= OnBaseBagOpenEvent;
        EventHandler.BaseBagCloseEvent -= OnBaseBagCloseEvent;
    }

    private void Awake()
    {
        
    }


    private void Start()
    {
        //给每一个格子序号
        for (int i = 0; i < boxSlots.Count; i++)
        {
            boxSlots[i].slotIndex = i;
        }
        bagOpened = bagUI.activeInHierarchy;
        //playerMoneyText.text = InventoryManager.Instance.playerMoney.ToString();
        for (int i = 0; i < playerBagSlots.Length; i++)
        {
            playerBagSlots[i].slotIndex = i;
        }
        for(int i=0;i<potSlots.Length;i++)
        {
            potSlots[i].slotIndex = i;
        }
        for(int i = 0; i < falskSlots.Length; i++)
        {
            falskSlots[i].slotIndex = i;
        }
        for(int i = 0; i < distllerSlots.Length; i++)
        {
            distllerSlots[i].slotIndex = i;
        }
        boxOpened = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            OpenBagUI();
        }
    }

    //private void OnShowTradeUI(ItemDetails item, bool isSell)
    //{
    //    tradeUI.gameObject.SetActive(true);
    //    //tradeUI.SetupExchangeUI(item, isSell);
    //}

    /// <summary>
    /// 打开通用包裹UI事件
    /// </summary>
    /// <param name="slotType"></param>
    /// <param name="bagData"></param>
    private void OnBaseBagOpenEvent(ContainerType slotType, InventoryBag_SO bagData)
    {
        GameObject prefab = slotType switch
        {
            //ContainerType.Bag => ,
            _ => null,
        };

        //生成背包UI
        baseBag.SetActive(true);

        boxSlots = new List<SlotUI>();

        for (int i = 0; i < bagData.itemList.Count; i++)
        {
            SlotUI slot = Instantiate(prefab, baseBag.transform.GetChild(0)).GetComponent<SlotUI>();
            slot.slotIndex = i;
            boxSlots.Add(slot);
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(baseBag.GetComponent<RectTransform>());

        if (slotType == ContainerType.Box)
        {
            //bagUI.GetComponent<RectTransform>().pivot = new Vector2(-0.5f, 0.5f);
            bagUI.SetActive(true);
            bagOpened = true;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(bagUI.GetComponent<RectTransform>());
        //更新UI显示
        OnUpdateInventoryUI(InventoryLocation.Box, bagData.itemList);
    }

    /// <summary>
    /// 关闭通用包裹UI事件
    /// </summary>
    /// <param name="slotType"></param>
    /// <param name="bagData"></param>
    private void OnBaseBagCloseEvent(ContainerType slotType, InventoryBag_SO bagData)
    {
        baseBag.SetActive(false);
        itemToolTip.gameObject.SetActive(false);
        UpdateSlotHightlight(-1);

        foreach (SlotUI slot in boxSlots)
        {
            Destroy(slot.gameObject);
        }
        boxSlots.Clear();

        if (slotType == ContainerType.Box)
        {
            bagUI.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            bagUI.SetActive(false);
            bagOpened = false;
        }
    }


    private void OnBeforeSceneUnloadedEvent()
    {
        UpdateSlotHightlight(-1);
    }


    /// <summary>
    /// 更新指定位置的Slot事件函数
    /// </summary>
    /// <param name="location">库存位置</param>
    /// <param name="list">数据列表</param>
    private void OnUpdateInventoryUI(InventoryLocation location, List<LegacyInventoryItem> list)
    {
        switch (location)
        {
            case InventoryLocation.Bag:
                for (int i = 0; i < playerBagSlots.Length; i++)
                {
                    if (list[i].itemAmount > 0)
                    {
                        LegacyItemDetails item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                        playerBagSlots[i].UpdateSlot(item, list[i].itemAmount);
                    }
                    else
                    {
                        playerBagSlots[i].UpdateEmptySlot();
                    }
                }
                break;
            case InventoryLocation.Box:
                for(int i = 0; i < list.Count; i++)
                {
                    if(list[i].itemAmount > 0)
                    {
                        SlotUI cell = ResManager.LoadResource<SlotUI>("Prefab/UI/Solt_bag");
                        cell.transform.parent = content;
                        LegacyItemDetails item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                        cell.UpdateSlot(item, list[i].itemAmount);
                    }
                }
                //for (int i = 0; i < boxSlots.Count; i++)
                //{
                    
                //    if (list[i].itemAmount > 0)
                //    {
                //        LegacyItemDetails item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                //        boxSlots[i].UpdateSlot(item, list[i].itemAmount);
                //    }
                //    else
                //    {
                //        boxSlots[i].UpdateEmptySlot();
                //    }
                //}
                break;
            case InventoryLocation.Flask:
                for (int i = 0; i < falskSlots.Length; i++)
                {
                    if (list[i].itemAmount > 0)
                    {
                        LegacyItemDetails item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                        falskSlots[i].UpdateSlot(item, list[i].itemAmount);
                    }
                    else
                    {
                        falskSlots[i].UpdateEmptySlot();
                    }
                }
                break;
            case InventoryLocation.Pot:
                for(int i = 0;i < potSlots.Length; i++)
                {
                    if (list[i].itemAmount > 0)
                    {
                        LegacyItemDetails item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                        potSlots[i].UpdateSlot(item, list[i].itemAmount);
                    }
                    else
                    {
                        potSlots[i].UpdateEmptySlot();
                    }
                }
                break;
            case InventoryLocation.Furnace:
                for(int i = 0; i < furnaceSlots.Length; i++)
                {
                    if (list[i].itemAmount > 0)
                    {
                        LegacyItemDetails item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                        furnaceSlots[i].UpdateSlot(item, list[i].itemAmount);
                    }
                    else
                    {
                        furnaceSlots[i].UpdateEmptySlot();
                    }
                }
                break;
            case InventoryLocation.Distiller:
                for(int i = 0; i < distllerSlots.Length; i++)
                {
                    if (list[i].itemAmount > 0)
                    {
                        LegacyItemDetails item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                        distllerSlots[i].UpdateSlot(item, list[i].itemAmount);
                    }
                    else
                    {
                        distllerSlots[i].UpdateEmptySlot();
                    }
                }
                break;
        }

        
    }

    /// <summary>
    /// 打开关闭背包UI，Button调用事件
    /// </summary>
    public void OpenBagUI()
    {
        bagOpened = !bagOpened;

        bagUI.SetActive(bagOpened);
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryManager.Instance.playerBag.itemList);
    }

    public void OpenBoxUI()
    {
        boxOpened = !boxOpened;

        baseBag.SetActive(boxOpened);
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, InventoryManager.Instance.boxBag.itemList);
    }


    /// <summary>
    /// 更新Slot高亮显示
    /// </summary>
    /// <param name="index">序号</param>
    public void UpdateSlotHightlight(int index)
    {
        foreach (SlotUI slot in boxSlots)
        {
            if (slot.isSelected && slot.slotIndex == index)
            {
                slot.slotHightlight.gameObject.SetActive(true);
            }
            else
            {
                slot.isSelected = false;
                slot.slotHightlight.gameObject.SetActive(false);
            }
        }
    }

}

using ProjectBase.Res;
using ProjectBase.UI;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class InventoryUI : MonoBehaviour
{
    public ItemToolTip itemToolTip;
    private string itemToolTipPath = "Prefab/UI/ItemToolTip";
    
    public ItemDataList_SO itemList;

    [Header("拖拽图片")]
    public Image dragItem;
    [Header("背包格子位置")]
    public Transform content;

    //[Header("玩家背包")]
    //[SerializeField] private GameObject bagUI;

    [Header("仓库")]
    [SerializeField] private GameObject baseBag;

    //[Header("交换UI")]
    //public ExchangeUI tradeUI;
    //public TextMeshProUGUI playerMoneyText;

    public List<SlotUI> boxSlots=new List<SlotUI>();
    [SerializeField] private SlotUI[] playerBagSlots;

    [Header("各类容器")]
    public SlotUI[] flaskSlots=new SlotUI[5];
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
        for (int i = 0; i < playerBagSlots.Length; i++)
        {
            playerBagSlots[i].slotIndex = i;
        }
        for(int i=0;i<potSlots.Length;i++)
        {
            potSlots[i].slotIndex = i;
        }
        //for(int i = 0; i < flaskSlots.Length; i++)
        //{
        //    flaskSlots[i].slotIndex = i;
        //}
        for(int i = 0; i < distllerSlots.Length; i++)
        {
            distllerSlots[i].slotIndex = i;
        }
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
    /// <param name="boxData"></param>
    private void OnBaseBagOpenEvent(ContainerType slotType, InventoryBag_SO boxData)
    {
        GameObject prefab = slotType switch
        {
            //ContainerType.Bag => ,
            _ => null,
        };

        //生成背包UI
        baseBag.SetActive(true);

        boxSlots = new List<SlotUI>();
        
        for (int i = 0; i < boxData.itemList.Count; i++)
        {
            SlotUI slot = Instantiate(prefab, baseBag.transform.GetChild(0)).GetComponent<SlotUI>();
            slot.slotIndex = i;
            boxSlots.Add(slot);
        }
        for (int i = 0; i < boxSlots.Count; i++)
        {
            boxSlots[i].slotIndex = i;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(baseBag.GetComponent<RectTransform>());

        if (slotType == ContainerType.Box)
        {
            ////bagUI.GetComponent<RectTransform>().pivot = new Vector2(-0.5f, 0.5f);
            //bagUI.SetActive(true);
        }
        //LayoutRebuilder.ForceRebuildLayoutImmediate(bagUI.GetComponent<RectTransform>());
        //更新UI显示
        OnUpdateInventoryUI(InventoryLocation.Box, boxData.itemList);
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
            //bagUI.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            //bagUI.SetActive(false);
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
                if (GameObject.Find("InventoryBK") == null) return;
                content = GameObject.Find("InventoryBK").transform;
                
                for (int i = 0; i < content.transform.childCount; i++)
                {
                    Transform transform = content.transform.GetChild(i);
                    Destroy(transform.gameObject);
                }
                boxSlots.Clear();

                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].itemAmount > 0)
                    {
                        SlotUI cell = ResManager.LoadResource<GameObject>("Prefab/UI/Solt_bag").GetComponent<SlotUI>();
                        if (content != null)
                        cell.transform.parent = content;
                        cell.slotType = ContainerType.Box;
                        boxSlots.Add(cell);
                        LegacyItemDetails item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                        cell.UpdateSlot(item, list[i].itemAmount);
                    }
                }
                for (int i = 0; i < boxSlots.Count; i++)
                {
                    boxSlots[i].slotIndex = i;
                }
                break;
            case InventoryLocation.Flask:
                if (!UIManager.Instance.panelsDic.ContainsKey("FlaskPanel")) return;
                foreach(SlotUI slot in UIManager.Instance.GetPanel<FlaskPanel>("FlaskPanel").falskSlots)
                {
                    
                    flaskSlots[slot.slotIndex]= slot;

                }
                for (int i = 0; i < flaskSlots.Length; i++)
                {
                    print(i);
                    if (list[i].itemAmount > 0)
                    {
                        LegacyItemDetails item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                        flaskSlots[i].UpdateSlot(item, list[i].itemAmount);
                    }
                    else
                    {
                        flaskSlots[i].UpdateEmptySlot();
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
        //UIManager.Instance.ShowPanel<BagPanel>
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, InventoryManager.Instance.playerBag.itemList);
    }

    public void OpenBoxUI()
    {
        UIManager.Instance.ShowPanel<BoxPanel>("BoxPanel", E_UI_Layer.top);
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, InventoryManager.Instance.boxBag.itemList);
    }


    /// <summary>
    /// 更新Slot高亮显示
    /// </summary>
    /// <param name="index">序号</param>
    public void UpdateSlotHightlight(int index)
    {
        //print(index);
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

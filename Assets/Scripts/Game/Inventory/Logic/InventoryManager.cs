using ProjectBase.Res;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Utilities;

public class InventoryManager : Utilities.Singleton<InventoryManager>
{
    [Header("神奇的表")]
    public List<DataItem> itemDetailsList;
    [Header("物品数据")]
    public ItemDataList_SO itemDataList_SO;
    [Header("读表")]
    public Materials materials_SO;
    public Potions potions_SO;
    [Header("背包数据")]
    public InventoryBag_SO playerBag;
    public InventoryBag_SO boxBag;
    public InventoryBag_SO potBag;
    public InventoryBag_SO flaskBag;
    public InventoryBag_SO furnaceBag;
    public InventoryBag_SO distillerBag;
    [Header("效果")]
    public InstantStats InstantEffectSO;
    public Stats StatsSO;
    [Header("配方")]
    public CauldronRecipes cauldronRecipes_SO;

    public int potionUseEffectRadius;

    [Header("是否拾取仪器")]
    public bool haveFlask;
    public bool haveDistiller;
    public bool haveFurnace;
    public bool havePot;
    protected override void Awake()
    {
        base.Awake();
        itemDetailsList = new List<DataItem>();
        ReadTable();
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, boxBag.itemList);
        //EventHandler.CallUpdateInventoryUI(InventoryLocation.Pot, potBag.itemList);
        //EventHandler.CallUpdateInventoryUI(InventoryLocation.Distiller, distillerBag.itemList);
        //DontDestroyOnLoad(this);
        DontDestroyOnLoad(this);
    }
    //目前没有表，先做测试
    public void ReadTable()
    {
        itemDetailsList.Clear();
        foreach(var item in itemDataList_SO.itemDetailsList)
        {
            DataItem itemDetails = new DataItem();
            itemDetails.ID = item.itemID;
            itemDetails.itemIcon = item.itemIcon;
            itemDetails.itemName = item.itemName;
            itemDetails.itemDescription = item.itemDescription;
            itemDetailsList.Add(itemDetails);
            //print(itemDetails.ID.BaseId);
        }
        
        //foreach(var itemID in materials_SO.MaterialEntities)
        //{
        //    if (HasTableContain(itemID.id)) continue;
        //    TrItem itemDetails = new TrItem();
        //    itemDetails.ID = itemID.id;
        //    itemDetails.itemName = itemID.materialName;
        //    itemDetails.itemDescription = itemID.description;
        //    itemDetails.itemType = ItemType.Material;
        //    itemDetails.itemUseRadius = potionUseEffectRadius;
        //    itemDetails.itemIcon = ReturnIcon(itemID.id);
        //    itemDetails.itemOnWorldSprite=ReturnIcon(itemID.id);
        //    itemDetailsList.Add(itemDetails);
        //}
        //foreach(var itemID in potions_SO.PotionEntities)
        //{
        //    if (HasTableContain(itemID.id)) continue;
        //    DataItem itemDetails = new DataItem();
        //    itemDetails.itemID = itemID.id;
        //    itemDetails.itemName = itemID.potionName;
        //    itemDetails.itemDescription = itemID.description;
        //    itemDetails.itemType = ItemType.Potion;
        //    itemDetails.itemUseRadius = potionUseEffectRadius;
        //    itemDetails.itemIcon = ReturnIcon(itemID.id);
        //    itemDetails.itemOnWorldSprite = ReturnIcon(itemID.id);
        //    itemDetails.foeverEffect = itemID.foreverEffectId;
        //    //itemDetails.effectsIDs.Add(itemID.toEnemyEffectIds);
        //    itemDetails.purity = 80;
        //    itemDetailsList.Add(itemDetails);
        //}
    }
    //private void Update()
    //{
    //    Debug.Log(ProjectBase.UI.UIManager.Instance);
    //}

    private bool HasTableContain(ItemID id)
    {
        foreach(var item in itemDetailsList)
        {
            if(EqualID(id,item.ID)) return true;
        }
        return false;
    }

    private Sprite ReturnIcon(int id)
    {
        Sprite sprite = null;
        if (id > 100 && id < 300)
        {
            sprite = Resources.Load<Sprite>("ToolUI/药水/" + id);
        }
        if (id > 300)
        {
            sprite = Resources.Load<Sprite>("ToolUI/药材/" + id);
        }
        if (sprite == null)
        {
            return Resources.Load<Sprite>("ToolUI/药水/" + "随便什么药水");
        }
        return sprite;
    }

    /// <summary>
    /// 通过ID返回物品信息
    /// </summary>
    /// <param name="ID">Item ID</param>
    /// <returns></returns>
    public DataItem GetItemDetails(ItemID ID)
    {
        return itemDetailsList.Find(i => EqualID(ID,i.ID));
    }
    public bool EqualID(ItemID id1,ItemID id2)
    {
        if(id1 == null || id2 == null) return false;
        if(id1.id == id2.id && id1.BaseId == id2.BaseId)
        {
            if(id1.ATTR.SequenceEqual(id2.ATTR)) return true;
        }
        return false;
    }
    public MaterialEntity GetMaterialEntity(int ID)
    {
        return materials_SO.MaterialEntities.Find(i => i.id==ID);
    }
    public PotionEntity GetPotionEntity(int ID)
    {
        return potions_SO.PotionEntities.Find(i => i.id == ID);
    }

   
    /// <summary>
    /// 通过物品ID添加物品到仓库
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="itemCount"></param>
    /// <param name="toDestory"></param>
    public void AddItem(ItemID itemId, int itemCount)
    {
        //是否已经有该物品
        int index = GetItemIndexInBox(itemId);
        AddItemAtIndex(itemId, index, itemCount);

        //更新UI
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, boxBag.itemList);
    }


    /// <summary>
    /// 检查背包是否有空位
    /// </summary>
    /// <returns></returns>
    private bool CheckBagCapacity()
    {
        for (int i = 0; i < playerBag.itemList.Count; i++)
        {
            if (playerBag.itemList[i].itemID.IsUnityNull())
                return true;
        }
        return false;
    }
    private bool CheckBoxCapacity()
    {
        for (int i = 0; i < boxBag.itemList.Count; i++)
        {
            if (boxBag.itemList[i].itemAmount==0)
                return true;
        }
        return false;
    }



    /// <summary>
    /// 通过物品ID找到背包已有物品位置
    /// </summary>
    /// <param name="ID">物品ID</param>
    /// <returns>-1则没有这个物品否则返回序号</returns>
    public int GetItemIndexInBag(ItemID ID)
    {
        for(int i = 0; i < playerBag.itemList.Count; i++)
        {

            if (EqualID(playerBag.itemList[i].itemID, ID))
                return i;
        }
        return -1;
    }
    public int GetItemIndexInBox(ItemID ID)
    {
        for(int i = 0; i < boxBag.itemList.Count; i++)
        {

            if (EqualID(boxBag.itemList[i].itemID, ID))
                return i;
        }
        return -1;
    }



    //// <summary>
    /// 给仓库添加物品
    /// </summary>
    /// <param name="ID">物品ID</param>
    /// <param name="index">序号</param>
    /// <param name="amount">数量</param>
    private void AddItemAtIndex(ItemID ID,int index,int amount)
    {
        if (index == -1)
        {
            InventoryItem item = new InventoryItem { itemID = ID, itemAmount = amount };
            boxBag.itemList.Add(item);
        }
        else
        {
            int currentAmount = boxBag.itemList[index].itemAmount + amount;
            InventoryItem item = new InventoryItem { itemID = ID, itemAmount = currentAmount };
            boxBag.itemList[index] = item;
        }
    }

   

    /// <summary>
    /// Player背包范围内交换物品
    /// </summary>
    /// <param name="fromIndex">起始序号</param>
    /// <param name="targetIndex">目标数据序号</param>
    public void SwapItem(int fromIndex, int targetIndex)
    {
        InventoryItem currentItem = playerBag.itemList[fromIndex];
        InventoryItem targetItem = playerBag.itemList[targetIndex];
        if (EqualID(currentItem.itemID, targetItem.itemID)) return;
        if (targetItem.itemAmount>0)
        {
            playerBag.itemList[fromIndex] = targetItem;
            playerBag.itemList[targetIndex] = currentItem;
        }
        else
        {
            playerBag.itemList[targetIndex] = currentItem;
            playerBag.itemList[fromIndex] = new InventoryItem();
        }

        EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, playerBag.itemList);
    }

    /// <summary>
    /// 跨背包交换数据
    /// </summary>
    /// <param name="locationFrom"></param>
    /// <param name="fromIndex"></param>
    /// <param name="locationTarget"></param>
    /// <param name="targetIndex"></param>
    public void SwapItem(InventoryLocation locationFrom, int fromIndex, InventoryLocation locationTarget, int targetIndex)
    {
        List<InventoryItem> currentList = GetItemList(locationFrom);
        List<InventoryItem> targetList = GetItemList(locationTarget);

        InventoryItem currentItem = currentList[fromIndex];

        if (targetIndex < targetList.Count)
        {
            InventoryItem targetItem = targetList[targetIndex];

            if (targetItem.itemAmount>0 && !EqualID(currentItem.itemID, targetItem.itemID))  //有不相同的两个物品
            {
                //print(targetIndex);
                currentList[fromIndex] = targetItem;
                targetList[targetIndex] = currentItem;
            }
            else if (EqualID(currentItem.itemID, targetItem.itemID))//相同的两个物品
            {
                targetItem.itemAmount += currentItem.itemAmount;
                targetList[targetIndex] = targetItem;
                //currentList[fromIndex] = new InventoryItem();
            }
            else    //目标空格子
            {
                targetList[targetIndex] = currentItem;
                if(locationFrom!= InventoryLocation.Box)
                    currentList[fromIndex] = new InventoryItem();
                else
                {
                    boxBag.itemList.Remove(currentItem);
                }
            }
            EventHandler.CallUpdateInventoryUI(locationFrom, currentList);
            EventHandler.CallUpdateInventoryUI(locationTarget, targetList);
        }
    }

    /// <summary>
    /// 根据位置返回背包数据列表
    /// </summary>
    /// <param name="location"></param>
    /// <returns></returns>
    private List<InventoryItem> GetItemList(InventoryLocation location)
    {
        return location switch
        {
            InventoryLocation.Bag => playerBag.itemList,
            InventoryLocation.Box=>boxBag.itemList,
            InventoryLocation.Pot=>potBag.itemList,
            InventoryLocation.Distiller=> distillerBag.itemList,
            InventoryLocation.Flask=> flaskBag.itemList,
            InventoryLocation.Furnace=> furnaceBag.itemList,
            _ => null
        };
    }

    /// <summary>
    /// 移除指定数量的仓库物品
    /// </summary>
    /// <param name="ID">物品ID</param>
    /// <param name="removeAmount">数量</param>
    public void RemoveItem(ItemID ID, int removeAmount)
    {
        var index = GetItemIndexInBox(ID);

        if (boxBag.itemList[index].itemAmount > removeAmount)
        {
            var amount = boxBag.itemList[index].itemAmount - removeAmount;
            var item = new InventoryItem { itemID = ID, itemAmount = amount };
            boxBag.itemList[index] = item;
        }
        else if (boxBag.itemList[index].itemAmount == removeAmount)
        {
            var item = new InventoryItem();
            boxBag.itemList[index] = item;
        }

        EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, boxBag.itemList);
    }

    /// <summary>
    /// 移除指定数量的背包物品
    /// </summary>
    /// <param name="ID">物品ID</param>
    /// <param name="removeAmount">数量</param>
    public void RemoveBagItem(ItemID ID, int removeAmount)
    {
        var index = GetItemIndexInBag(ID);
        if (playerBag.itemList[index].itemAmount > removeAmount)
        {
            var amount = playerBag.itemList[index].itemAmount - removeAmount;
            var item = new InventoryItem { itemID = ID, itemAmount = amount };
            playerBag.itemList[index] = item;
        }
        else if (playerBag.itemList[index].itemAmount == removeAmount)
        {
            var item = new InventoryItem();
            playerBag.itemList[index] = item;
        }

        //EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, playerBag.itemList);
    }

    /// <summary>
    /// 药水预览
    /// </summary>
    /// <param name="m1">材料1</param>
    /// <param name="m2">材料2</param>
    public int ReturnProductionID(int m1, int m2)
    {
        foreach (var VARIABLE1 in cauldronRecipes_SO.CauldronRecipeEntities)
        {
            if (m1 == VARIABLE1.materialId_1 && m2 == VARIABLE1.materialId_2)
            {
                return VARIABLE1.productionId;
            }
        }
        return 301;
    }

    public void PutAllIn()
    {
        for(int i = 0; i < playerBag.itemList.Count; i++)
        {
            InventoryItem item = playerBag.itemList[i];
            if(item.itemAmount>0)
            {
                AddItem(item.itemID, item.itemAmount);
                InventoryItem empty=new InventoryItem();
                playerBag.itemList[i]= empty;
            }
        }
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag,playerBag.itemList);
    }
    /// <summary>
    /// 物品放回仓库
    /// </summary>
    public void ItemBackBox(InventoryLocation locationFrom, int fromIndex)
    {
        List<InventoryItem> currentList = GetItemList(locationFrom);
        InventoryItem currentItem = currentList[fromIndex];
        AddItem(currentItem.itemID, currentItem.itemAmount);
        InventoryItem empty=new InventoryItem();
        currentList[fromIndex]=empty;
        EventHandler.CallUpdateInventoryUI(locationFrom,currentList);
    }
}
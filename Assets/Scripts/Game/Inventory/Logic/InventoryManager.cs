using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using Utilities;

public class InventoryManager : Singleton<InventoryManager>
{
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
    private void Start()
    {
        ReadTable();
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Box, boxBag.itemList);
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Pot, potBag.itemList);
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Distiller, distillerBag.itemList);
        //DontDestroyOnLoad(this);
    }

    public void ReadTable()
    {
        //itemDataList_SO.itemDetailsList.Clear();
        foreach(var item in materials_SO.MaterialEntities)
        {
            if (HasTableContain(item.id)) continue; 
            ItemDetails itemDetails = new ItemDetails();
            itemDetails.itemID = item.id;
            itemDetails.itemName = item.materialName;
            itemDetails.itemDescription = item.description;
            itemDetails.itemType = ItemType.Material;
            itemDetails.itemUseRadius = potionUseEffectRadius;
            itemDetails.itemIcon = ReturnIcon(item.id);
            itemDetails.itemOnWorldSprite=ReturnIcon(item.id);
            itemDataList_SO.itemDetailsList.Add(itemDetails);
        }
        foreach(var item in potions_SO.PotionEntities)
        {
            if (HasTableContain(item.id)) continue;
            ItemDetails itemDetails = new ItemDetails();
            itemDetails.itemID = item.id;
            itemDetails.itemName = item.potionName;
            itemDetails.itemDescription = item.description;
            itemDetails.itemType = ItemType.Potion;
            itemDetails.itemUseRadius = potionUseEffectRadius;
            itemDetails.itemIcon = ReturnIcon(item.id);
            itemDetails.itemOnWorldSprite = ReturnIcon(item.id);
            itemDetails.foeverEffect = item.foreverEffectId;
            //itemDetails.effectsIDs.Add(item.toEnemyEffectIds);
            itemDetails.purity = 80;
            itemDataList_SO.itemDetailsList.Add(itemDetails);
        }
    }

    private bool HasTableContain(int id)
    {
        foreach(var item in itemDataList_SO.itemDetailsList)
        {
            if(item.itemID == id) return true;
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
    public ItemDetails GetItemDetails(int ID)
    {
        return itemDataList_SO.itemDetailsList.Find(i => i.itemID == ID);
    }
    public MaterialEntity GetMaterialEntity(int ID)
    {
        return materials_SO.MaterialEntities.Find(i=>i.id == ID);
    }
    public PotionEntity GetPotionEntity(int ID)
    {
        return potions_SO.PotionEntities.Find(i=>i.id == ID);
    }

    /// <summary>
    /// 添加物品到Player背包里
    /// </summary>
    /// <param name="item"></param>
    /// <param name="toDestory">是否要销毁物品</param>
    public void AddItem(Item item, bool toDestory)
    {
        //是否已经有该物品
        int index = GetItemIndexInBag(item.itemID);

        AddItemAtIndex(item.itemID, index, 1);

        if (toDestory)
        {
            Destroy(item.gameObject);
        }

        //更新UI
        EventHandler.CallUpdateInventoryUI(InventoryLocation.Bag, playerBag.itemList);
    }
    /// <summary>
    /// 通过物品ID添加物品
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="itemCount"></param>
    /// <param name="toDestory"></param>
    public void AddItem(int itemId, int itemCount)
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
            if (playerBag.itemList[i].itemID == 0)
                return true;
        }
        return false;
    }
    private bool CheckBoxCapacity()
    {
        for (int i = 0; i < boxBag.itemList.Count; i++)
        {
            if (boxBag.itemList[i].itemID == 0)
                return true;
        }
        return false;
    }



    /// <summary>
    /// 通过物品ID找到背包已有物品位置
    /// </summary>
    /// <param name="ID">物品ID</param>
    /// <returns>-1则没有这个物品否则返回序号</returns>
    public int GetItemIndexInBag(int ID)
    {
        for (int i = 0; i < playerBag.itemList.Count; i++)
        {
            if (playerBag.itemList[i].itemID == ID)
                return i;
        }
        return -1;
    }
    public int GetItemIndexInBox(int ID)
    {
        for(int i = 0; i < boxBag.itemList.Count; i++)
        {
            if (boxBag.itemList[i].itemID == ID)
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
    private void AddItemAtIndex(int ID, int index, int amount)
    {
        if (index == -1 && CheckBoxCapacity())
        {
            InventoryItem item = new InventoryItem { itemID = ID, itemAmount = amount };
            for (int i = 0; i < boxBag.itemList.Count; i++)
            {
                if (boxBag.itemList[i].itemID == 0)
                {
                    boxBag.itemList[i] = item;
                    break;
                }
            }
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
        if (currentItem.itemID == targetItem.itemID) return;
        if (targetItem.itemID != 0)
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

            if (targetItem.itemID != 0 && currentItem.itemID != targetItem.itemID)  //有不相同的两个物品
            {
                currentList[fromIndex] = targetItem;
                targetList[targetIndex] = currentItem;
            }
            else if (currentItem.itemID == targetItem.itemID) //相同的两个物品
            {
                targetItem.itemAmount += currentItem.itemAmount;
                targetList[targetIndex] = targetItem;
                currentList[fromIndex] = new InventoryItem();
            }
            else    //目标空格子
            {
                targetList[targetIndex] = currentItem;
                currentList[fromIndex] = new InventoryItem();
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
    public void RemoveItem(int ID, int removeAmount)
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
    public void RemoveBagItem(int ID, int removeAmount)
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
}
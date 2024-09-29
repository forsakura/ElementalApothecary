using ProjectBase.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FightingUIPanel : BasePanel, IScrollHandler
{
    private Player player;

    private Image childBar;
    private Image parentBar;
    private Image bulletBar;
    private Image elementPointer;

    public SlotUI[] bagSlots;
    public SlotUI[] bulletSlots;
    private List<InventoryItem> itemList;
    public int usingBagIndex;
    public int usingBulletIndex;

    int maxCount;
    int currentCount;

    int maxHealth;
    int currentHealth;

    const float delayTime = 0.5f;
    float timer = 0;
    protected override void Awake()
    {
        base.Awake();
    }
    

    void Start()
    {
        player = PlayerController.Instance?.GetComponent<Player>();
        maxHealth = player.characterData.maxHealth;
        player.OnHealthChange += OnPlayerHealthChange;
        childBar = GetControl<Image>("ChildBar");
        parentBar = GetControl<Image>("HealthBar");
        bulletBar = GetControl<Image>("BulletBar");
        elementPointer = GetControl<Image>("ElementPointer");
        maxCount = 10;
        currentCount = 0;
        bulletBar.fillAmount = 1.0f * currentCount / maxCount;
        player.OnFill += OnBulletFill;
        player.OnShoot += OnBulletUse;
        GetControl<Button>("LeftMoveBag").onClick.AddListener(() =>
        {
            usingBagIndex = (usingBagIndex - 1+itemList.Count) % itemList.Count;
            ChangeBagSlot(usingBagIndex);
        });
        GetControl<Button>("RightMoveBag").onClick.AddListener(() =>
        {
            usingBagIndex = (usingBagIndex + 1) % itemList.Count;
            ChangeBagSlot(usingBagIndex);
        });
        GetControl<Button>("LeftMoveBullet").onClick.AddListener(() =>
        {
            usingBulletIndex = (usingBulletIndex - 1 + itemList.Count) % itemList.Count;
            ChangeBulletSlot(usingBulletIndex);
        });
        GetControl<Button>("RightMoveBullet").onClick.AddListener(() =>
        {
            usingBulletIndex = (usingBulletIndex + 1) % itemList.Count;
            ChangeBulletSlot(usingBulletIndex);
        });
        
        itemList = InventoryManager.Instance.playerBag.itemList;
        usingBagIndex = 0;
        usingBulletIndex = 0;
        ChangeBagSlot(usingBagIndex % itemList.Count);
        ChangeBulletSlot(usingBulletIndex % itemList.Count);

        PlayerInputManager.Instance.FightUI.SwitchLeft.started += BulletLeftSwitch;
        PlayerInputManager.Instance.FightUI.SwitchRight.started += BulletRightSwitch;
    }

    /// <summary>
    /// 切换正在使用的背包物品，暂时没想好怎么写，先用着
    /// </summary>
    /// <param name="index"></param>
    public void ChangeBagSlot(int index)
    {
        index = (index - 1 + itemList.Count) % itemList.Count;
        for (int i = 0; i < bagSlots.Length; i++)
        {
            DataItem _item = InventoryManager.Instance.GetItemDetails(itemList[index].itemID);

            if (itemList[index].itemAmount > 0)
            {
                bagSlots[i].UpdateSlot(_item, itemList[index].itemAmount);
            }
            else
            {
                bagSlots[i].UpdateEmptySlot();
            }
            index = (index + 1) % itemList.Count;

        }
    }

    public void ChangeBulletSlot(int index)
    {
        index = (index - 1 + itemList.Count) % itemList.Count;
        for (int i = 0; i < bulletSlots.Length; i++)
        {
            DataItem _item = InventoryManager.Instance.GetItemDetails(itemList[index].itemID);

            if (itemList[index].itemAmount > 0)
            {
                bulletSlots[i].UpdateSlot(_item, itemList[index].itemAmount);
            }
            else
            {
                bulletSlots[i].UpdateEmptySlot();
            }
            index = (index + 1) % itemList.Count;

        }
    }

    private void OnBulletFill(Characters go, int max)
    {
        maxCount = max;
        currentCount = maxCount;
        bulletBar.fillAmount = 1.0f * currentCount / maxCount;
    }

    private void OnBulletUse(Characters go, int useCount)
    {
        currentCount -= useCount;
        bulletBar.fillAmount = 1.0f * currentCount / maxCount;
    }
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (childBar.fillAmount > parentBar.fillAmount)
        {
            parentBar.fillAmount = childBar.fillAmount;
        }
        else if (childBar.fillAmount < parentBar.fillAmount && timer <= 0)
        {
            parentBar.fillAmount -= 0.005f;
        }
        elementPointer.rectTransform.sizeDelta = new Vector2(player.elementState.elementVector.magnitude,25);


        elementPointer.rectTransform.localEulerAngles = new Vector3(0, 0, Vector2.Angle(player.elementState.elementVector, new Vector2(1, 0)));
    }

   

    private void OnPlayerHealthChange(Characters go, int health)
    {
        currentHealth = health;
        childBar.fillAmount = 1.0f * currentHealth / maxHealth;
        timer = delayTime;
    }

    public void OnScroll(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public DataItem GetCurrentBullet()
    {
        if(itemList[usingBulletIndex].itemAmount < 1) return null;
        InventoryItem item = new InventoryItem();
        item.itemID = itemList[usingBulletIndex].itemID;
        item.itemAmount = itemList[usingBulletIndex].itemAmount-1;
        itemList[usingBulletIndex] = item;
        print(usingBulletIndex);
        DataItem itemDetails = InventoryManager.Instance.GetItemDetails(item.itemID);
        bulletSlots[1].UpdateSlot(itemDetails, item.itemAmount);
        return itemDetails;
    }
    public DataItem GetCurrentItem()
    {
        if (itemList[usingBagIndex].itemAmount < 1) return null;
        InventoryItem item = new InventoryItem();
        item.itemID = itemList[usingBagIndex].itemID;
        item.itemAmount = itemList[usingBagIndex].itemAmount - 1;
        itemList[usingBagIndex] = item;
        DataItem itemDetails = InventoryManager.Instance.GetItemDetails(item.itemID);
        bagSlots[1].UpdateSlot(itemDetails, item.itemAmount);
        return itemDetails;
    }

    private void BulletLeftSwitch(InputAction.CallbackContext context)
    {
        usingBulletIndex = (usingBulletIndex - 1 + itemList.Count) % itemList.Count;
        ChangeBulletSlot(usingBulletIndex);
    }
    private void BulletRightSwitch(InputAction.CallbackContext context)
    {
        usingBulletIndex = (usingBulletIndex + 1) % itemList.Count;
        ChangeBulletSlot(usingBulletIndex);
    }
}

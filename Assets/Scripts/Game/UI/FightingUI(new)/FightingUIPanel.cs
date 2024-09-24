using ProjectBase.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightingUIPanel : BasePanel
{
    private Player player;

    private Image childBar;
    private Image parentBar;
    private Image bulletBar;
    private Image elementPointer;

    public SlotUI[] bagSlots;
    private List<InventoryItem> itemList;
    public int usingBagIndex;

    int maxCount;
    int currentCount;

    int maxHealth;
    int currentHealth;

    const float delayTime = 0.5f;
    float timer = 0;
    protected override void Awake()
    {
        base.Awake();
        itemList = InventoryManager.Instance.playerBag.itemList;
        usingBagIndex = 0;
        ChangeBagSlot(usingBagIndex%itemList.Count);
    }

    public void ChangeBagSlot(int index)
    {
        for (int i = 0; i < bagSlots.Length; i++)
        {
            DataItem _item = InventoryManager.Instance.GetItemDetails(itemList[index].itemID);
            
            if (itemList[index].itemAmount>0)
            {
                bagSlots[i].UpdateSlot(_item, itemList[index].itemAmount);
            }
            else
            {
                bagSlots[i].UpdateEmptySlot();
            }
            index=(index+1)%itemList.Count;
            
        }
    }

    void Start()
    {
        player = PlayerController.Instance?.GetComponent<Player>();
        maxHealth = player.characterData.MaxHealth;
        player.OnHealthChange += OnPlayerHealthChange;
        childBar = GetControl<Image>("ChildBar");
        parentBar = GetControl<Image>("HealthBar");
        bulletBar = GetControl<Image>("BulletBar");
        elementPointer = GetControl<Image>("ElementPointer");
        maxCount = 1;
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
}

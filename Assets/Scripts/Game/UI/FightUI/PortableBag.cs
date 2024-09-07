using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utilities;
using static UnityEditor.Progress;


public class PortableBag : Singleton<PortableBag>
{
    [HideInInspector]public Slot currentSlot;
    [HideInInspector]public PlayerControll playerControll;
    int currentSlotCount;
    public List<Slot> slots;

    protected override void Awake() 
    {
        base.Awake();
        playerControll = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControll>();
        // test
        // InitBag();
    }
    private void Start()
    {
        InitBag();
    }
    private void Update() 
    {
        SwitchSlotByKeyboard();   
    }

    public void InitBag()
    {
        Dictionary<int, int> potions = new Dictionary<int, int>();
        var itemlist = InventoryManager.Instance.playerBag.itemList;
        foreach (var item in itemlist)
        {
            if (item.itemID.id==0)
            {
                //potions.Add(itemID.itemID, itemID.itemAmount);
            }
        }
        InitBag(potions);
    }
    // public void InitBag(Dictionary<int, int> potions)
    public void InitBag(Dictionary<int, int> potions)
    {
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    transform.GetChild(i).GetComponent<Slot>().InitSlot(i);
        //}
        int i = 0;
        foreach (var item in potions)
        {
            //transform.GetChild(i).GetComponent<Slot>().InitSlot(itemID.Key, itemID.Value);
            i++;
            // ����9���������Ĳ�Ҫ��
            if(i >= 10)
            {
                break;
            }
        }
        // �������9��ʣ�µĿ���
        if (potions.Count < 10)
        {
            for (i = potions.Count;  i < 10; i++)
            {
                //transform.GetChild(i).GetComponent<Slot>().InitSlot(-1, 0);
            }
        }
        currentSlot = transform.GetChild(0).GetComponent<Slot>();
    }

    public void SetCurrentSlot(Slot slot)
    {
        currentSlot = slot;
    }

    //public int GetCurrentPotionID()
    //{
    //    return currentSlot.PotionId;
    //}

    public void SetPotionCount(int count)
    {
        currentSlot.SetCountTo(count);
    }

    public void SwitchSlotByKeyboard()
    {
        if (transform.childCount <= 0) return;

        if (Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            currentSlot = transform.GetChild(0).GetComponent<Slot>();
        }
        else if (Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            currentSlot = transform.GetChild(1).GetComponent<Slot>();
        }
        else if (Keyboard.current.digit3Key.wasPressedThisFrame)
        {
            currentSlot = transform.GetChild(2).GetComponent<Slot>();
        }
        else if (Keyboard.current.digit4Key.wasPressedThisFrame)
        {
            currentSlot = transform.GetChild(3).GetComponent<Slot>();
        }
        else if (Keyboard.current.digit5Key.wasPressedThisFrame)
        {
            currentSlot = transform.GetChild(4).GetComponent<Slot>();
        }
        else if (Keyboard.current.digit6Key.wasPressedThisFrame)
        {
            currentSlot = transform.GetChild(5).GetComponent<Slot>();
        }
        else if (Keyboard.current.digit7Key.wasPressedThisFrame)
        {
            currentSlot = transform.GetChild(6).GetComponent<Slot>();
        }
        else if (Keyboard.current.digit8Key.wasPressedThisFrame)
        {
            currentSlot = transform.GetChild(7).GetComponent<Slot>();
        }
        else if (Keyboard.current.digit9Key.wasPressedThisFrame)
        {
            currentSlot = transform.GetChild(8).GetComponent<Slot>();
        }
        else if (Keyboard.current.digit0Key.wasPressedThisFrame)
        {
            currentSlot = transform.GetChild(9).GetComponent<Slot>();
        }
        if (currentSlot != null)
        {
            currentSlot.SetCurrentSelected();
        }
    }

    //public void Test()
    //{
    //    int potionID = GetCurrentPotionID();
    //    SetPotionCount(10);
    //    Debug.Log(potionID);
    //}
}

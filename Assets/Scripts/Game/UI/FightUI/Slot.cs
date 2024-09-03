using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    PortableBag bag;
    [SerializeField]
    public Image potionIcon;
    [SerializeField]
    TextMeshProUGUI count;

    [HideInInspector]
    public int potionCounts;

    public ItemID PotionId
    {
        get;
        private set;
    }

    string messagePanelPath = "Prefab/UI/FightUI/PotionMessagePanel";
    GameObject messagePanel = null;


    // public void InitSlot(int potionId, int count)
    // ?????????????
    // ?????????????
    public void InitSlot(ItemID potionID, int potionCount)
    {
        if (potionCount == 0 || potionID.id == 0)
        {
            PotionId = new ItemID();
            this.count.text = "";
            potionCounts = 0;
            potionIcon.gameObject.SetActive(false);
            return;
        }
        DataItem item = InventoryManager.Instance.GetItemDetails(potionID);
        potionIcon.gameObject.SetActive(true);
        PotionId = potionID;
        //potionIcon.sprite = itemID.itemIcon;
        // potionIcon.sprite = Resources.Load<Sprite>($"ToolUI/药水/{potionID}");
        count.text = potionCount.ToString();
        potionCounts = potionCount;
    }

    public void SetCountTo(int count)
    {
        potionCounts = count;
        if (count == 0)
        {
            PotionId = new ItemID();
            this.count.text = "";
            potionIcon.gameObject.SetActive(false);
            return;
        }
        this.count.text = count.ToString();
    }

    //public int Get()
    //{
    //    if (potionCount == 0)
    //    {
    //        return null;
    //    }
    //    potionCount--;
    //    if (potionCount == 0)
    //    {
    //        count.text = "";
    //        potionIcon.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        count.text = potionCount.ToString();
    //    }
    //    return potionPrefab;
    //}

    public void SetCurrentSelected()
    {
        GetComponent<Toggle>().isOn = true;
    }

    public void OnValueChanged(bool isSelect)
    {
        if (isSelect)
        {
            bag.SetCurrentSlot(this);
        }
    }

    // ??? 1s ???????????
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (PotionId.id==0)
        {
            CreateMessagePanel();
            StartCoroutine(CountDownAndStartDisplay());
        }

        PortableBag.Instance.playerControll.useMedicine = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        if (messagePanel != null)
        {
            messagePanel.GetComponent<PotionMessagePanel>().FadeOut();
        }
        messagePanel = null;

        PortableBag.Instance.playerControll.useMedicine = false;
    }

    private void CreateMessagePanel()
    {
        messagePanel = GameObject.Instantiate(Resources.Load<GameObject>(messagePanelPath));
        messagePanel.transform.SetParent(transform);
        //messagePanel.GetComponent<PotionMessagePanel>().InitPanel(PotionId);
    }

    private IEnumerator CountDownAndStartDisplay()
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime;
            yield return null;
        }

        messagePanel.GetComponent<PotionMessagePanel>().FadeIn();
    }
}

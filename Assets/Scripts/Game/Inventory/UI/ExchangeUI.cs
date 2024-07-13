using System;
using UnityEngine;
using UnityEngine.UI;


public class ExchangeUI : MonoBehaviour
{
    public Image itemIcon;
    public Text itemName;
    public InputField tradeAmount;
    public Button submitButton;
    public Button cancelButton;

    private ItemDetails item;

    private void Awake()
    {
        cancelButton.onClick.AddListener(CancelExchange);
        submitButton.onClick.AddListener(ExchangeItem);
    }

    /// <summary>
    /// 设置UI显示详情
    /// </summary>
    /// <param name="item"></param>
    /// <param name="isSell"></param>
    public void SetupExchangeUI(ItemDetails item)
    {
        this.item = item;
        itemIcon.sprite = item.itemIcon;
        itemName.text = item.itemName;
        tradeAmount.text = string.Empty;
    }

    private void ExchangeItem()
    {
        int amount = Convert.ToInt32(tradeAmount.text);


        CancelExchange();
    }


    private void CancelExchange()
    {
        this.gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.Linq;
using Utilities;

public class BulletConsumption : Singleton<BulletConsumption>
{
    [SerializeField]
    Image potionIcon;
    [SerializeField]
    TextMeshProUGUI potionName;
    [SerializeField]
    Image cover;

    public int maxBullet = 10;

    private void Start()
    {
        // 只要遮罩把后面的药水名字和图标遮住了，那就是没有！
        cover.fillAmount = 1.0f;
    }

    // 装填子弹
    // public void Fill(Sprite potion)
    public void Fill(int potionID)
    {
        ItemDetails itemdetails = InventoryManager.Instance.GetItemDetails(potionID);
        cover.fillAmount = 0.0f;
        potionIcon.sprite = itemdetails.itemIcon;
        potionName.text = itemdetails.itemName;
    }

    public void UpdateCover(int remainingBullet)
    {
        cover.fillAmount = 1 - remainingBullet / maxBullet;
    }
}
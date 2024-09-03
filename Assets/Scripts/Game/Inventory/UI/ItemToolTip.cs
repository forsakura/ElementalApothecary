using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI BottomPart;

    public void SetupTooltip(DataItem itemDetails, ContainerType slotType)
    {
        if (itemDetails == null) return;
        nameText.text = itemDetails.itemName;
        //typeText.text = GetItemType(itemDetails.tags);
        descriptionText.text = itemDetails.itemDescription;
        //BottomPart.text ="纯度："+ itemDetails.purity;
       
        LayoutRebuilder.ForceRebuildLayoutImmediate(GetComponent<RectTransform>());
    }
    private string GetItemType(ItemType itemType)
    {
        return itemType switch
        {
            ItemType.Potion => "药水",
            ItemType.Material=>"材料",
            ItemType.Special=>"特殊材料",
            _=>"其他"
        };
    }
}

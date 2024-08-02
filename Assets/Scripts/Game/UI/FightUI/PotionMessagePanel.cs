using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PotionMessagePanel : MonoBehaviour
{
    [SerializeField]
    Image icon;
    [SerializeField]
    TextMeshProUGUI p_name;
    [SerializeField]
    TextMeshProUGUI p_purage;
    [SerializeField]
    TextMeshProUGUI p_Aer;
    [SerializeField]
    TextMeshProUGUI p_Igins;
    [SerializeField]
    TextMeshProUGUI p_Aqua;
    [SerializeField]
    TextMeshProUGUI p_Terra;
    [SerializeField]
    TextMeshProUGUI p_Desctiption;
    // And many ToDo

    Coroutine coroutine;
    CanvasGroup canvasGroup;
    float alpha;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup == null )
        {
            gameObject.AddComponent<CanvasGroup>();
        }
        alpha = 0.0f;
        canvasGroup.alpha = alpha;
        GetComponent<RectTransform>().offsetMin = Vector2.zero;
        GetComponent<RectTransform>().offsetMax = Vector2.zero;
    }

    public void InitPanel(ItemID potionID)
    {
        foreach(var item in InventoryManager.Instance.potions_SO.PotionEntities)
        {
            //if (item.id == potionID)
            {
                LegacyItemDetails itemdetails = InventoryManager.Instance.GetItemDetails(potionID);
                icon.sprite = itemdetails.itemIcon;
                p_name.text = item.potionName;
                p_purage.text = $"纯度：{item.purity}";
                p_Aer.text = $"风：{item.aerNum}";
                p_Igins.text = $"火：{item.ignisNum}";
                p_Aqua.text = $"水：{item.aquaNum}";
                p_Terra.text = $"土：{item.terraNum}";
                p_Desctiption.text = item.description;
                return;
            }
        }
    }

    public void FadeIn()
    {
        StopAllCoroutines();
        coroutine = StartCoroutine(PanelFadeIn());
    }

    public void FadeOut()
    {
        StopAllCoroutines();
        coroutine = StartCoroutine(PanelFadeOut());
    }

    private IEnumerator PanelFadeIn()
    {
        while (alpha < 1.0f)
        {
            canvasGroup.alpha = alpha;
            yield return null;
            alpha += Time.deltaTime * 2;
        }
        canvasGroup.alpha = alpha;
    }

    private IEnumerator PanelFadeOut()
    {
        while(alpha > 0.0f)
        {
            canvasGroup.alpha = alpha;
            yield return null;
            alpha -= Time.deltaTime * 2;
        }
        canvasGroup.alpha = alpha;
        Destroy(gameObject);
    }
}

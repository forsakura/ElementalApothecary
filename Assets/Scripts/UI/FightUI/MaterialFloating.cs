using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class MaterialFloating : MonoBehaviour
{
    public Image MaterialIcon;
    public TextMeshProUGUI Count;

    private CanvasGroup canvasGroup;
    private MaterialFloatingContainer container;

    private float timer = 0.0f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if(canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    private void Update()
    {
        CountDown();
    }

    private void CountDown()
    {
        timer += Time.deltaTime;
        if (timer < 0.5f)
        {
            canvasGroup.alpha = timer * 2;
        }
        else if(timer > 3.0f)
        {
            container.floatings.Dequeue();
            container.UpdateRect();
            Destroy(gameObject);
        }
        else if(timer > 2.5f)
        {
            canvasGroup.alpha = (3.0f - timer) * 2;
        }
    }

    public void InitSet(int materialID, int count, MaterialFloatingContainer parent)
    {
        ItemDetails item = InventoryManager.Instance.GetItemDetails(materialID);
        MaterialIcon.sprite = item.itemIcon;
        Count.text = "x" + count;
        container = parent;
    }
}

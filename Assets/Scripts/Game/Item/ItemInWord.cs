using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInWord : MonoBehaviour
{
    public ItemID itemID;
    private Sprite spriteOnWorld;
    public Vector2 worldPos;
    void Start()
    {
        spriteOnWorld=InventoryManager.Instance.GetItemDetails(itemID).itemIcon;
        //print(spriteOnWorld);
        gameObject.GetComponent<SpriteRenderer>().sprite = spriteOnWorld;
    }
    public void SetPos()
    {
        transform.position = worldPos;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            InventoryManager.Instance.AddItem(itemID, 1);
            Destroy(gameObject);
        }
    }
}

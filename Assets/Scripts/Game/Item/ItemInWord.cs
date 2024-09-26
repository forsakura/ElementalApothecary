using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInWord : MonoBehaviour
{
    public ItemID itemID;
    private Sprite spriteOnWorld;
    private SpriteRenderer spriteRenderer;
    public Vector2 worldPos;
    private BoxCollider2D coll;
    void Start()
    {
        spriteOnWorld=InventoryManager.Instance.GetItemDetails(itemID).itemIcon;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite=spriteOnWorld;
        coll=GetComponent<BoxCollider2D>();
        //Vector2 newSize = new Vector2(spriteRenderer.sprite.bounds.size.x, spriteRenderer.sprite.bounds.size.y);
        //coll.size = newSize;
        //coll.offset = new Vector2(0, spriteRenderer.sprite.bounds.center.y);
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

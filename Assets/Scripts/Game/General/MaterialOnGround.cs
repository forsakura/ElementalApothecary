using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialOnGround : MonoBehaviour
{
    public int materialID;
    public int count;

    SpriteRenderer spriteRenderer;

    [SerializeField]
    Materials materialCollection;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }
    }

    public void InitMaterial(int id, int count)
    {
        foreach (var material in materialCollection.MaterialEntities)
        {
            if (material.id == materialID)
            {
                spriteRenderer.sprite = Resources.Load<Sprite>($"ToolUI/Ò©²Ä/{materialID}");
                materialID = id;
                this.count = count;
            }
        }
    }

    public MaterialEntity GetMaterial()
    {
        foreach (var material in materialCollection.MaterialEntities)
        {
            if (material.id == materialID)
            {
                return material;
            }
        }
        return null;
    }
}

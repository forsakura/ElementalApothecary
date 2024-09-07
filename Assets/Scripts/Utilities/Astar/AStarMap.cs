using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarPathFinding
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AStarMap : MonoBehaviour
    {
        [SerializeField]
        public new BoxCollider2D collider;

        void Awake()
        {
            collider = GetComponent<BoxCollider2D>();
            Vector2 offset = collider.offset + new Vector2(transform.position.x, transform.position.y);
            AStarManager.Instance.InitMap((int)(offset.x - collider.size.x * transform.localScale.x / 2), (int)(offset.x + collider.size.x * transform.localScale.x / 2 + 1), (int)(offset.y - collider.size.y * transform.localScale.y / 2), (int)(offset.y + collider.size.y * transform.localScale.y / 2 + 1));
        }
    }
}
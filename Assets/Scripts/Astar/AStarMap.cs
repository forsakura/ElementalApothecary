using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStarPathFinding
{
    public class AStarMap : MonoBehaviour
    {
        [SerializeField]
        public new Collider2D collider;
        [SerializeField]
        float width;
        [SerializeField]
        float height;

        void Awake()
        {
            Vector2 offset = collider.offset + new Vector2(transform.position.x, transform.position.y);
            AStarManager.Instance.InitMap((int)(offset.x - width / 2), (int)(offset.x + width / 2 + 1), (int)(offset.y - height / 2), (int)(offset.y + height / 2 + 1));
        }
    }
}
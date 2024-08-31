using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AStarPathFinding
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AStarObstacle : MonoBehaviour
    {
        public new BoxCollider2D collider;
        // Start is called before the first frame update
        void Start()
        {
            collider = GetComponent<BoxCollider2D>();
            Vector2 newOffset = collider.offset + new Vector2(transform.position.x, transform.position.y);
            for(int i = Mathf.RoundToInt(newOffset.x - collider.size.x * transform.localScale.x / 2); i < Mathf.RoundToInt(newOffset.x + collider.size.x * transform.localScale.x / 2); i++)
            {
                for (int j = Mathf.RoundToInt(newOffset.y - collider.size.y * transform.localScale.y / 2); j < Mathf.RoundToInt(newOffset.y + collider.size.y * transform.localScale.y / 2); j++)
                {
                    AStarManager.Instance.SetCost(new Vector2Int(i, j), 999);
                }
            }
        }


    }
}

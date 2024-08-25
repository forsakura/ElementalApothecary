using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy;

public class EnemyTest : EnemyBase
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 ?next = AStarPathFinding.AStarManager.Instance.GetNext(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), new Vector2Int(Mathf.FloorToInt(target.x), Mathf.FloorToInt(target.y)));
        if (next != null) 
        {
            rb.velocity = characterData.MoveSpeed * ((Vector2)next - new Vector2(transform.position.x, transform.position.y)).normalized;
        }
    }
}

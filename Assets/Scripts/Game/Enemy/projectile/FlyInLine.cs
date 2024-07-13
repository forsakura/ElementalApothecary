using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyInLine : MonoBehaviour
{
    [SerializeField]
    float speed;

    private Vector2 direction = Vector2.zero;
    private float timer = 0.0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (direction != Vector2.zero)
        {
            rb.velocity = direction * speed;
        }
        timer += Time.deltaTime;
        if (timer > 10.0f)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 target)
    {
        direction = (target - (Vector2)transform.position).normalized;
    }
}

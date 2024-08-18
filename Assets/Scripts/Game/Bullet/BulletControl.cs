using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public HitInstance hitInstance;

    bool initFinish = false;
    BulletType bulletType;
    Vector2 start;
    Vector2 target;

    Rigidbody2D rb;
    float timer = 0.0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        start = rb.position;
    }

    private void Update()
    {
        if (initFinish)
        {
            switch (bulletType)
            {
                case BulletType.Throw:
                    transform.position = Parabola.ClaculateCurrentPoint(start, target, timer, 1.0f, 2.0f);
                    if (timer > 1.0f)
                    {
                        Destroy(gameObject);
                        // 此处生成范围碰撞箱
                    }
                    break;
                case BulletType.Shoot:
                    rb.velocity = (target - start).normalized * 10.0f; //dir * speed
                    if (timer > 10.0f)
                    {
                        Destroy(gameObject);
                    }
                    break;
            }
        }
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletType == BulletType.Throw)
        {
            return;
        }
        if (collision.gameObject != hitInstance.Source && (collision.tag.Equals("Player") || collision.tag.Equals("Enemy")))
        {
            if (collision.gameObject.GetComponent<Characters>().GetHit(hitInstance))
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetBullet(Vector2 target, HitInstance hit, BulletType type)
    {
        this.target = target;
        this.hitInstance = hit;
        this.bulletType = type;
        initFinish = true;
    }
}


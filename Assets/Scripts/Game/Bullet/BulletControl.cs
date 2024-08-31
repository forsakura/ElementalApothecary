using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BulletControl : MonoBehaviour
{
    bool initFinish = false;
    BulletType bulletType;
    Vector2 start;
    Vector2 target;

    Rigidbody2D rb;
    float timer = 0.0f;

    public delegate void OnBulletHitEventHandler(BulletControl bullet, Collider2D collision);
    public event OnBulletHitEventHandler OnBulletHitTarget;

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
                        OnBulletHitTarget?.Invoke(this, null);
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
        OnBulletHitTarget?.Invoke(this, collision);
    }

    public void SetBullet(Vector2 target, BulletType type)
    {
        this.target = target;
        this.bulletType = type;
        initFinish = true;
    }
}


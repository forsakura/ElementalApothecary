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

    public delegate void OnBulletHitEventHandler(BulletControl bullet, Characters go);
    public event OnBulletHitEventHandler OnShootHitTarget;
    public event OnBulletHitEventHandler OnThrowHitTarget;

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
                        List<Characters> characters = CharacterManager.Instance.GetAllCharacters();
                        foreach (var item in characters)
                        {
                            if ((item.transform.position - transform.position).magnitude < 2.0f)
                            {
                                Debug.Log("BulletThrowHit");
                                OnThrowHitTarget?.Invoke(this, item);
                            }
                        }
                        Destroy(gameObject);
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
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletType == BulletType.Throw)
        {
            return;
        }
        if (collision.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            return;
        }
        OnShootHitTarget?.Invoke(this, collision.GetComponent<Characters>());
    }

    public void SetBullet(Vector2 target, BulletType type)
    {
        this.target = target;
        this.bulletType = type;
        initFinish = true;
    }
}


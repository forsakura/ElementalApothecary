using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Frog
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class FrogAnimation : MonoBehaviour
    {
        private Vector2 speedDir = Vector2.zero;
        private Rigidbody2D rb;
        private Frog frog;
        private Animator anim;

        private float dir = 1.0f;

        private float timer = 0.0f;

        public ContactFilter2D contactFilter;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            frog = GetComponent<Frog>();
            anim = GetComponent<Animator>();
            IdleStart();
        }

        private void FixedUpdate()
        {
            rb.velocity = speedDir * frog.characterData.MoveSpeed;
        }

        private void Update()
        {
            if (rb.velocity.x > 0.0f)
            {
                dir = 1.0f;
            }
            else if (rb.velocity.x < 0.0f)
            {
                dir = -1.0f;
            }
            anim.SetFloat("Direction", dir);

            if (timer > 0.0f)
            {
                timer -= Time.deltaTime;
                if (timer <= 0.0f)
                {
                    anim.SetTrigger("Jump");
                }
            }
        }

        private void Move()
        {
            // 
            if (frog.getTaunt)
            {
                Vector2? next = AStarPathFinding.AStarManager.Instance.GetNext(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), new Vector2Int(Mathf.FloorToInt(frog.target.transform.position.x), Mathf.FloorToInt(frog.target.transform.position.y)));
                if (next != null)
                {
                    speedDir = ((Vector2)next - new Vector2(transform.position.x, transform.position.y)).normalized;
                }
                else
                {
                    speedDir = Vector2.zero;
                }
            }
            else
            {
                int targetNumbers;
                RaycastHit2D[] hit2D = new RaycastHit2D[5];
                speedDir = Vector2.zero;
                int count = 0;
                do
                {
                    speedDir = Random.insideUnitCircle;
                    // target = (Vector2)transform.position + Random.insideUnitCircle * enemyData.WalkMaxDistance;
                    targetNumbers = Physics2D.Raycast(transform.position, speedDir, contactFilter, hit2D, 2 * frog.characterData.MoveSpeed);
                    // Debug.Log(targetNumbers);
                    count++;
                } while (targetNumbers != 0 && count < 10);
                //speedDir = Random.insideUnitCircle.normalized;
            }
        }

        private void Stop()
        {
            speedDir = Vector2.zero;
        }

        private void IdleStart()
        {
            timer = frog.getTaunt ? 1.0f : Random.Range(3.0f, 5.0f);
        }
    }
}

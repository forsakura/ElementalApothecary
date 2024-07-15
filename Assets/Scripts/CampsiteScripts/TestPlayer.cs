using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float moveSpeed = 5f; // 玩家移动的速度

    private Rigidbody2D rb; // 玩家的Rigidbody组件

    private void Start()
    {
        // 获取玩家对象上的Rigidbody组件
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // 获取水平和垂直输入
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // 根据输入计算移动向量
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // 移动玩家
        rb.velocity = moveDirection * moveSpeed;
    }
}

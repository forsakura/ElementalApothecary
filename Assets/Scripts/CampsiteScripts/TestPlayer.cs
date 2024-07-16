using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    public float moveSpeed = 5f; // ����ƶ����ٶ�

    private Rigidbody2D rb; // ��ҵ�Rigidbody���

    private void Start()
    {
        // ��ȡ��Ҷ����ϵ�Rigidbody���
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        // ��ȡˮƽ�ʹ�ֱ����
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // ������������ƶ�����
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;

        // �ƶ����
        rb.velocity = moveDirection * moveSpeed;
    }
}

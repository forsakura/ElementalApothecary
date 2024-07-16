using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��������ҽ����Ķ������
/// </summary>
public abstract class PlayerInteraction : MonoBehaviour
{
    public GameObject signUI;//������־
    public bool canOpenUI => signUI.activeSelf;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            signUI.SetActive(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            signUI.SetActive(false);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 可以与玩家交互的对象基类
/// </summary>
public abstract class PlayerInteraction : MonoBehaviour
{
    public GameObject signUI;//交互标志
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 可以与玩家交互的对象基类
/// </summary>
public abstract class PlayerInteraction : MonoBehaviour
{
    public GameObject InteractTip;//交互标志

    private GameObject tip;

    public void ShowInteractTip()
    {
        if (tip == null)
        {
            tip = Instantiate(InteractTip, new Vector3(transform.position.x, transform.position.y + 1.0f * transform.localScale.y, 1.0f), new Quaternion(), transform);

        }
    }
    public void HideInteractTip()
    {
        if (tip != null)
        {
            Destroy(tip);
        }
    }

    public abstract void Interact();

}

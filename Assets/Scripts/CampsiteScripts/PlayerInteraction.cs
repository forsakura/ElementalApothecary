using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ��������ҽ����Ķ������
/// </summary>
public abstract class PlayerInteraction : MonoBehaviour
{
    public GameObject InteractTip;//������־

    private GameObject tip;

    public void ShowInteractTip()
    {
        tip = Instantiate(InteractTip, new Vector3(transform.position.x, transform.position.y + 1.0f * transform.localScale.y), new Quaternion(), transform);
    }
    public void HideInteractTip()
    {
        Destroy(tip);
    }

    public abstract void Interact();

}

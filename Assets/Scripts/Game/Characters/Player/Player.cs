using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ��Ҷ��еķ�������ҩ
/// ��Ҷ��ֱ۵Ŀ���
/// </summary>

public class Player : Characters
{
    public GameObject PlayerArm;

    private PlayerInteraction interactableObject;

    // �˴��߼�Ϊ����һ�����ɽ�����Ϊ��ǰ�������뿪ǰ������һ������Ḳ��ԭ���ģ���Ϊ�µ�
    // ��ʱ���뿪ԭ���ģ���Ϊԭ���������ڵĿɽ����ﲻͬ�����Դ�ʱ��������ɽ�������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteraction interactObject = collision.gameObject.GetComponent<PlayerInteraction>();
        if (interactObject != null)
        {
            //��ֹ��Ӷ��
            if (interactableObject != null)
            {
                interactableObject.HideInteractTip();
            }
            else
            {
                PlayerInputManager.Instance.GamePlay.Interact.started += Interact;
            }
            interactableObject = interactObject;
            interactableObject.ShowInteractTip();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteraction interactObject = collision.gameObject.GetComponent<PlayerInteraction>();
        if (interactObject == null)
        {
            return;
        }
        if (interactObject == interactableObject)
        {
            PlayerInputManager.Instance.GamePlay.Interact.started -= Interact;
            interactableObject.HideInteractTip();
            this.interactableObject = null;
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if(interactableObject != null)
        {
            interactableObject.Interact();
        }
    }

    public void Drink()
    {

    }
}

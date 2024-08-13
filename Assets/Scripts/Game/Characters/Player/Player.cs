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

    private IInteractable interactableObject;

    // �˴��߼�Ϊ����һ�����ɽ�����Ϊ��ǰ�������뿪ǰ������һ������Ḳ��ԭ���ģ���Ϊ�µ�
    // ��ʱ���뿪ԭ���ģ���Ϊԭ���������ڵĿɽ����ﲻͬ�����Դ�ʱ��������ɽ�������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactObject = collision.gameObject.GetComponent<IInteractable>();
        if (interactObject != null)
        {
            //��ֹ��Ӷ��
            PlayerInputManager.Instance.GamePlay.Interact.started -= Interact;
            PlayerInputManager.Instance.GamePlay.Interact.started += Interact;
            interactableObject = interactObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactObject = collision.gameObject.GetComponent<IInteractable>();
        if (interactObject == interactableObject)
        {
            PlayerInputManager.Instance.GamePlay.Interact.started -= Interact;
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

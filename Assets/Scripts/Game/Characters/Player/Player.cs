using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ��Ҷ��еķ�������ҩ
/// ��Ҷ��ֱ۵Ŀ���
/// </summary>

public class Player : Characters
{
    public GameObject PlayerArm;

    // ��ǰ�ύ��������
    private PlayerInteraction interactableObject;
    private List<PlayerInteraction> interactableObjects;

    private void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefab/Bullets/Bullet");
        interactableObjects = new List<PlayerInteraction>();
    }

    // ʹ��List�洢�������壬��������List����뿪ĳ��ʱ�����Ƴ�����Ϊ���һ��������ʾ�Ƴ�������һ���Ľ�����ʾ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteraction interactObject = collision.gameObject.GetComponent<PlayerInteraction>();
        if (interactObject == null)
        {
            return;
        }
        if (interactableObjects.Count != 0)
        {
            // ���ص�ǰ
            interactableObject.HideInteractTip();
        }
        else
        {
            PlayerInputManager.Instance.GamePlay.Interact.started += Interact;
        }
        interactableObject = interactObject;
        interactableObjects.Add(interactableObject);
        interactableObject.ShowInteractTip();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerInteraction interactObject = collision.gameObject.GetComponent<PlayerInteraction>();
        if (interactObject == null)
        {
            return;
        }
        // �뿪�ɽ������崥������Χ
        interactObject.HideInteractTip();
        interactableObjects.Remove(interactObject);
        if (interactableObjects.Count == 0)
        {
            PlayerInputManager.Instance.GamePlay.Interact.started -= Interact;
            this.interactableObject = null;
        }
        else
        {
            interactableObject = interactableObjects.Last();
            interactableObject.ShowInteractTip();
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

    public void Fill()
    {

    }
}

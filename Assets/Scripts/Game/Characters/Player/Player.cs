using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 玩家独有的方法：喝药
/// 玩家对手臂的控制
/// </summary>

public class Player : Characters
{
    public GameObject PlayerArm;

    // 当前会交互的物体
    private PlayerInteraction interactableObject;
    private List<PlayerInteraction> interactableObjects;

    private void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefab/Bullets/Bullet");
        interactableObjects = new List<PlayerInteraction>();
    }

    // 使用List存储交互物体，后进入的在List最后，离开某个时将其移除，若为最后一个，则显示移出后倒数第一个的交互提示
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteraction interactObject = collision.gameObject.GetComponent<PlayerInteraction>();
        if (interactObject == null)
        {
            return;
        }
        if (interactableObjects.Count != 0)
        {
            // 隐藏当前
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
        // 离开可交互物体触发器范围
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

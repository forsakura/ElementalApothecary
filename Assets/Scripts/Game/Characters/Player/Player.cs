using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 玩家独有的方法：喝药
/// 玩家对手臂的控制
/// </summary>

public class Player : Characters
{
    public GameObject PlayerArm;

    private PlayerInteraction interactableObject;

    // 此处逻辑为进入一个，可交互变为当前，后若离开前进入另一个，则会覆盖原来的，变为新的
    // 此时若离开原来的，因为原来的与现在的可交互物不同，所以此时不会清除可交互内容
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInteraction interactObject = collision.gameObject.GetComponent<PlayerInteraction>();
        if (interactObject != null)
        {
            //防止添加多个
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

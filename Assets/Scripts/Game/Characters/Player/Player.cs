using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using CharacterDelegates;
using ProjectBase.UI;
using UnityEngine.SceneManagement;

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

    public event DrinkEventHandler OnDrink;

    private void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefab/Bullets/Bullet");
        interactableObjects = new List<PlayerInteraction>();
        OnDeath += OnPlayerDead;
        //DontDestroyOnLoad(gameObject);
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

    public void OnPlayerDead(Characters go, HitInstance hit)
    {
        //todo
        transform.position = new(18.17f, 5.33f, 0);
        CurrentHealth = characterData.MaxHealth;
        isDead = false;
        // Debug.Log(UIManager.Instance.GetPanel<FightingUIPanel>("FightingUI"));
        UIManager.Instance.HidePanel("FightingUI");
        SceneManager.LoadScene("Campsite");
    }
    


    public override void OnShootHitTarget(BulletControl bullet, Collider2D go)
    {
        if (go.transform.parent == null || go == null)
        {
            return;
        }
        if (go.transform.parent.CompareTag("Enemy"))
        {

            HitInstance hitInstance = new()
            {
                Source = gameObject,
                Damage = characterData.Damage,
                elementState = new ElementVector()
                {
                    elementVector = currentBulletValue.ReturnElementVector()
                }
            };
            if (go.GetComponent<IHitable>().GetHit(hitInstance))
            {
                Destroy(bullet.gameObject);
            }
        }
    }

    public override void OnThrowHitTarget(BulletControl bullet, Collider2D go)
    {
        if (go == null)
        {
            return;
        }
        HitInstance hitInstance = new()
        {
            Source = gameObject,
            Damage = characterData.Damage
        };
        go.GetComponent<HitArea>().GetHit(hitInstance);
    }

    public void Drink()
    {
        OnDrink?.Invoke(this);
    }
}

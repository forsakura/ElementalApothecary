using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour
{
    public CharacterData characterData;
    public int CurrentHealth;
    public float CurrentSpeed;
    public int[] ElementContain;
    public EElement[] ElementName = new EElement[2];

    public bool isInvincible;

    private void Awake()
    {
        CurrentHealth = characterData.MaxHealth;
        CurrentSpeed = characterData.MoveSpeed;
        ElementContain = new int[2] { 0, 0 };
        ElementName = new EElement[2] {EElement.None, EElement.None};

        isInvincible = false;
    }

    public virtual void GetHit(HitInstance hit)
    {
        if (hit == null)
        {
            return;
        }
        if(!hit.IgnoreInvincible && isInvincible)
        {
            return;
        }

    }
}

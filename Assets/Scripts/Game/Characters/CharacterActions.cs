using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions
{
    public Action<Characters, HitInstance> BeforeGetHit = (go, e) =>
    {
        Debug.Log("Before damage calculate action. " + go.name + " received hit from " + e.Source.name + ", damage value is " + e.Damage + ".");
    };
    public Action<Characters, HitInstance> AfterGetHit = (go, e) =>
    {
        Debug.Log("After damage calculate action. Damage value " + go.name + " received now is " + e.Damage + ".");
    };
    public Action<Characters, HitInstance> OnCharacterDeath = (go, e) =>
    {
        Debug.Log(go.name + "is killed by: " + e.Source.name);
    };

    public Action OnSwitchWeapon = () => 
    {
        Debug.Log("Switch weapon.");
    };
    public Action OnShoot = () =>
    {
        Debug.Log("Shoot.");
    };
    public Action OnThrow = () =>
    {
        Debug.Log("Throw.");
    };
    public Action OnDrink = () =>
    {
        Debug.Log("Drink.");
    };

    // ×óÓÒÑ¡ÔñÒ©Ë®
    public Action SwitchToLeftPotion = () => 
    {
        Debug.Log("Switch potion to left.");
    };

    public Action SwitchToRightPotion = () =>
    {
        Debug.Log("Switch potion to right.");
    };
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterActions
{
    public static Action<GameObject, HitInstance> BeforeGetHit = (go, e) => 
    {
        Debug.Log("Before damage calculate action. " + go.name + " received hit from " + e.Source.name + ", damage value is " + e.Damage + ".");
    };
    public static Action<GameObject, HitInstance> AfterGetHit = (go, e) =>
    {
        Debug.Log("After damage calculate action. Damage value " + go.name + " received now is " + e.Damage + ".");
    };
    public static Action<GameObject, HitInstance> OnCharacterDeath = (go, e) =>
    {
        Debug.Log(go.name + "is killed by: " + e.Source.name);
    };

    public static Action SwitchWeapon = () => 
    {
        Debug.Log("Switch weapon.");
    };
    public static Action OnShoot = () =>
    {
        Debug.Log("Shoot.");
    };
    public static Action OnThrow = () =>
    {
        Debug.Log("Throw.");
    };
    public static Action OnDrink = () =>
    {
        Debug.Log("Drink.");
    };

    // ?????????
    public static Action SwitchToLeftPotion = () => 
    {
        Debug.Log("Switch potion to left.");
    };

    public static Action SwitchToRightPotion = () =>
    {
        Debug.Log("Switch potion to right.");
    };
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerActions
{
    public static Action<HitInstance> BeforeGetHit;
    public static Action<HitInstance> AfterGetHit;

    public static Action SwitchWeapon;
    public static Action OnShoot;
    public static Action OnThrow;
    public static Action OnDrink;

    public static Action<PotionEntity> SwitchPotion; // ?
}

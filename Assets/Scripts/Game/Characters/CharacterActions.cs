using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterDelegates
{
    public delegate void BeforeGetHitEventHandler(Characters go, HitInstance hit);
    public delegate void AfterGetHitEventHandler(Characters go, HitInstance hit);
    public delegate void CharacterDeathEventHandler(Characters go, HitInstance hit);
    public delegate void OnHealthChangeEventHandler(Characters go, int health);
    public delegate void ShootEventHandler(Characters go, int useBulletCount);
    public delegate void ThrowEventHandler(Characters go);
    public delegate void DrinkEventHandler(Characters go);
    public delegate void FillBulletEventHandler(Characters go, int maxBulletCount);
    public delegate void SwitchLeftPotionEventHandler();
    public delegate void SwitchRightPotionEventHandler();

    public delegate void OnEnemyTargetSetEventHandler();
}
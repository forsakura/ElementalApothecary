using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterDelegates
{
    public delegate void BeforeGetHitEventHandler(Characters go, HitInstance hit);
    public delegate void AfterGetHitEventHandler(Characters go, HitInstance hit);
    public delegate void CharacterDeathEventHandler(Characters go, HitInstance hit);
    public delegate void ShootEventHandler();
    public delegate void ThrowEventHandler();
    public delegate void DrinkEventHandler();
    public delegate void FillBulletEventHandler();
    public delegate void SwitchLeftPotionEventHandler();
    public delegate void SwitchRightPotionEventHandler();
}
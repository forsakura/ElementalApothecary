
using UnityEngine;

[System.Serializable]
public class CharacterRestrictionEntity 
{
    [Tooltip("Lv")]public float Lv;
    [Tooltip("最大血量")]public float maxHp;
    [Tooltip("最大行走速度")]public float maxWalkSpeed;
    [Tooltip("最大无敌时间")]public float maxInvincibleTime;
    [Tooltip("最大投掷范围")]public float maxThrowDistance;
    [Tooltip("最大爆炸范围")]public float maxExplodeRadius;
    [Tooltip("最大射击速度")]public float maxShootSpeed;
    [Tooltip("使用时间消耗")]public float maxUseTimeInterval;
    [Header("元素")]
    [Tooltip("最大风 土元素量")]public float maxAerTerra;
    [Tooltip("最大水 火元素量")]public float maxIgnisAqua;

}

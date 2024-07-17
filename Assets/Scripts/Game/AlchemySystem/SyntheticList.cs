using System.Collections;
using System.Collections.Generic;
using FrameWork;
using FrameWork.Base;
using UnityEngine;

public class SyntheticList : MonoSingleton<SyntheticList>
{
    [Header("配方表")]
    public CauldronRecipes cauldronRecipes;

    [Header("药水表")]
    public Potions potions;

    [Header("素材表")]
    public Materials materials;

    [Header("状态表")]
    public Stats stats;

    [Header("永久效果表")]
    public ForeverEffects foreverEffects;

    [Header("瞬发状态表")]
    public InstantStats instantStats;
}

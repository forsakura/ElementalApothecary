using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物波
/// </summary>
[CreateAssetMenu(fileName = "EnemyWave",menuName = "ScriptableObject/New EnemyWave")]
public class EnemyWave : ScriptableObject
{
    [Header("当前波怪物种类")]
    public GameObject[] enmey;
    
    [Header("当前波怪物数量")]
    public int count;
    
    [Header("怪物生成间隔时间")]
    public float generateBetweenTime;
    
    [Header("怪物刷新区域中心点")]
    public Transform centerPoint;
    
    [Header("范围上侧边缘")]
    public float upOffset;
    
    [Header("范围下侧边缘")]
    public float downOffset;
    
    [Header("范围左侧边缘")]
    public float LeftOffset;
    
    [Header("范围右侧边缘")]
    public float RightOffset;
    
    [Header("怪物生成点的集合"),HideInInspector]
    public List<Vector3> enemyPositions;
}

using System;
using System.Collections;
using System.Collections.Generic;
using FrameWork;
using FrameWork.Base;
using UnityEngine;
using Random = UnityEngine.Random;

public enum areaName
{
    EnterArea,
    CaveArea,
    TrapArea,
    JungleArea,
    UndergroundArea
}
public class EnemyWaveManager : MonoSingleton<EnemyWaveManager>
{
    public List<EnemyWave> enemyWaves;
    
    /// <summary>
    /// 怪物波字典，一个区域对应一个怪物波
    /// </summary>
    public Dictionary<areaName, EnemyWave> waveDic = new Dictionary<areaName, EnemyWave>();

    private void Start()
    {
        foreach (areaName VARIABLE in Enum.GetValues(typeof(areaName)))
        {
            int index = (int)VARIABLE;
            waveDic.Add(VARIABLE,enemyWaves[index]);
        }
    }
    
    private IEnumerator GenerateEnemyWaveCoroutine(areaName type)
    {
        if (waveDic[type] == null) yield break;
        EnemyWave currentWave = waveDic[type];
        PositionCaculator(currentWave);
        for (int i = 0; i < currentWave.count; i++)
        {
            int value = Random.Range(0, currentWave.enmey.Length);
            GameObject enemy = Instantiate(currentWave.enmey[value], currentWave.enemyPositions[i], Quaternion.identity);
            yield return new WaitForSeconds(currentWave.generateBetweenTime);
        }
    }
    
    /// <summary>
    /// 位置生成器，避免重复位置
    /// </summary>
    /// <param name="wave">需要传入的怪物波</param>
    void PositionCaculator(EnemyWave wave)
    {
        var position = wave.centerPoint.position;
        while (wave.enemyPositions.Count < wave.count)
        {
            var x = Random.Range(position.x - wave.downOffset, position.x + wave.upOffset);
            var y = Random.Range(position.y - wave.LeftOffset, position.y + wave.RightOffset);
            var pos = new Vector3(x, y);
            if (!wave.enemyPositions.Contains(pos))
            {
                wave.enemyPositions.Add(new Vector3(x,y));
            }
        }
    }
}

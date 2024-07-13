using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObject/EnemyData")]
public class EnemyData : ScriptableObject
{
    public float SpanBetweenWalk;
    public float WalkDuration;
    public float WalkMaxDistance;
}

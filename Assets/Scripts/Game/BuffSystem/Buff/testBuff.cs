using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffTest", menuName = "Buffs/Buff Test")]
public class testbuff : BuffBase
{
    public testbuff() : base()
    {

    }

    public override void OnApply(GameObject target)
    {
        Debug.Log("ADD!!!");
    }

    public override void OnExpired(GameObject target)
    {
        Debug.Log("Expired!!!");
    }

    public override void OnUpdate(GameObject target, float deltaTime)
    {
        Debug.Log("TEST!!!!");
    }
}

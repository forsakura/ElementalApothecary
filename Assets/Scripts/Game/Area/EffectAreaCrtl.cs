using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class EffectAreaCrtl : MonoBehaviour
{
    [SerializeField]
    private float effectRadius = 0.5f; // 效果半径  
    [SerializeField]
    private float duration = 5f; // 效果持续时间  
    [SerializeField]
    private float interval = 1f; // 施加效果的时间间隔  

    [SerializeField]
    HitInstance hit = null;

    private List<Characters> targets = new();
    private Timer timer;

    public HitInstance Hit { get => hit; set => hit = value; }

    private void Awake()
    {
        // 初始化 Timer  
        timer = new Timer(duration, interval, OnEnter, OnInterval, OnEnd);

        transform.localScale = new Vector3(effectRadius, effectRadius, effectRadius);
    }

    private void OnEnter()
    {
        // 当目标进入效果区域时调用  
        Debug.Log("Effect started for targets in the area.");
    }

    private void OnInterval()
    {
        // 每个间隔施加效果  
        foreach (Characters target in targets)
        {
            Debug.Log("Applying effect to: " + target.name);
            target.GetHit(hit);
        }
    }

    private void OnEnd()
    {
        // 效果结束时调用  
        Debug.Log("Effect ended for targets in the area.");
        foreach (Characters target in targets)
        {
            
        }
        targets.Clear(); // 清空目标列表  
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Characters character = collider.gameObject.GetComponent<Characters>();
        if (character != null && !targets.Contains(character))
        {
            targets.Add(character);
            Debug.Log("New target added: " + character.name);
            timer.ResetTime(); // 重置计时器  
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Characters character = collider.gameObject.GetComponent<Characters>();
        if (character != null)
        {
            targets.Remove(character);
            Debug.Log("Target removed: " + character.name);
            if (targets.Count == 0)
            {
                timer.ResetTime(); // 如果没有目标，重置计时器  
            }
        }
    }
}

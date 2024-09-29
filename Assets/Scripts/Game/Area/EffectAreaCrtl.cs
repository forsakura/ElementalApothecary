using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class EffectAreaCrtl : MonoBehaviour
{
    [SerializeField]
    private float effectRadius = 0.5f; // Ч���뾶  
    [SerializeField]
    private float duration = 5f; // Ч������ʱ��  
    [SerializeField]
    private float interval = 1f; // ʩ��Ч����ʱ����  

    [SerializeField]
    HitInstance hit = null;

    private List<Characters> targets = new();
    private Timer timer;

    public HitInstance Hit { get => hit; set => hit = value; }

    private void Awake()
    {
        // ��ʼ�� Timer  
        timer = new Timer(duration, interval, OnEnter, OnInterval, OnEnd);

        transform.localScale = new Vector3(effectRadius, effectRadius, effectRadius);
    }

    private void OnEnter()
    {
        // ��Ŀ�����Ч������ʱ����  
        Debug.Log("Effect started for targets in the area.");
    }

    private void OnInterval()
    {
        // ÿ�����ʩ��Ч��  
        foreach (Characters target in targets)
        {
            Debug.Log("Applying effect to: " + target.name);
            target.GetHit(hit);
        }
    }

    private void OnEnd()
    {
        // Ч������ʱ����  
        Debug.Log("Effect ended for targets in the area.");
        foreach (Characters target in targets)
        {
            
        }
        targets.Clear(); // ���Ŀ���б�  
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Characters character = collider.gameObject.GetComponent<Characters>();
        if (character != null && !targets.Contains(character))
        {
            targets.Add(character);
            Debug.Log("New target added: " + character.name);
            timer.ResetTime(); // ���ü�ʱ��  
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
                timer.ResetTime(); // ���û��Ŀ�꣬���ü�ʱ��  
            }
        }
    }
}

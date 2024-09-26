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

    private List<Character> targets = new();
    private Timer timer;

    public HitInstance Hit { get => hit; set => hit = value; }

    private void Awake()
    {
        // ��ʼ�� Timer  
        timer = new Timer(duration, interval, OnEnter, OnInterval, OnEnd);
    }

    private void OnEnter()
    {
        // ��Ŀ�����Ч������ʱ����  
        Debug.Log("Effect started for targets in the area.");
    }

    private void OnInterval()
    {
        // ÿ�����ʩ��Ч��  
        foreach (Character target in targets)
        {
            Debug.Log("Applying effect to: " + target.name);
            //target.ApplyEffect();
        }
    }

    private void OnEnd()
    {
        // Ч������ʱ����  
        Debug.Log("Effect ended for targets in the area.");
        foreach (Character target in targets)
        {
            //target.RemoveEffect(); // �ָ�Ŀ���״̬  
        }
        targets.Clear(); // ���Ŀ���б�  
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Character character = collider.gameObject.GetComponent<Character>();
        if (character != null && !targets.Contains(character))
        {
            targets.Add(character);
            Debug.Log("New target added: " + character.name);
            timer.ResetTime(); // ���ü�ʱ��  
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Character character = collider.gameObject.GetComponent<Character>();
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

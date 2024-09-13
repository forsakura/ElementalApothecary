using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本质上就是个buff系统，但主要负责的不是buff的能力
/// </summary>
public class ATTRManager : MonoBehaviour
{
    private List<BaseAttribute> activeATTR = new List<BaseAttribute>();
    public GameObject m_gameObject;

    public void Add(BaseAttribute attribute)
    {
        attribute.OnApply(m_gameObject);
        activeATTR.Add(attribute);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = activeATTR.Count - 1; i >= 0; i--)
        {
            activeATTR[i].OnUpdate(m_gameObject, deltaTime);
            if (!activeATTR[i].IsPermanent)
            {
                if (activeATTR[i].IsExpired())
                {
                    activeATTR[i].OnExpired(m_gameObject);
                    activeATTR.RemoveAt(i);
                }
            }
        }
    }

    /// <summary>
    /// 处理对外
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}

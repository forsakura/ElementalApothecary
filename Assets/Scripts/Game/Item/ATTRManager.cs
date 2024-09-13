using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ͼ��Ǹ�buffϵͳ������Ҫ����Ĳ���buff������
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
    /// �������
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}

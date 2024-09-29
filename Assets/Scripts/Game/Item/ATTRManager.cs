using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����Ͼ��Ǹ�buffϵͳ������Ҫ����Ĳ���buff������
/// </summary>
public class ATTRManager : MonoBehaviour,IATTRManager
{
    private List<BaseAttribute> activeATTR = new List<BaseAttribute>();

    public void Add(BaseAttribute attribute)
    {
        attribute.OnApply(gameObject);
        activeATTR.Add(attribute);

        // ����Э�̴�������Ե���������  
        StartCoroutine(HandleAttribute(attribute));
    }

    private IEnumerator HandleAttribute(BaseAttribute attribute)
    {
        while (true)
        {
            // ��������  
            float deltaTime = Time.deltaTime;
            attribute.OnUpdate(gameObject, deltaTime);

            // ��������Ƿ����  
            if (!attribute.IsPermanent && attribute.IsExpired())
            {
                attribute.OnExpired(gameObject);
                activeATTR.Remove(attribute);
                yield break; // ����Э��  
            }

            // �ȴ���һ֡  
            yield return null;
        }
    }

    /// <summary>  
    /// �������  
    /// </summary>  
    /// <param name="collision"></param>  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ��ײ�����߼�  
    }
}

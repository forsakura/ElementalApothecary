using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϊʵ�����Ļ��Ǹ���buff����Ҳ�ǲο�������ʵ�ʹ����ϲ���Ӧ�����buff
/// </summary>
public class ATTRManager : MonoBehaviour
{
    private List<Attribute> activeATTR = new List<Attribute>();

    public void AddBuff(Attribute buff, GameObject target)
    {
        buff.Apply(target);
        activeATTR.Add(buff);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = activeATTR.Count - 1; i >= 0; i--)
        {
            if (!activeATTR[i].IsPermanent)
            {
                activeATTR[i].Update(gameObject, deltaTime);
                if (activeATTR[i].IsExpired())
                {
                    activeATTR.RemoveAt(i);
                }
            }
        }
    }
}

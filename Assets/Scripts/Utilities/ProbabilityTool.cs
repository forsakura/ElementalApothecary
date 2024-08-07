using ProjectBase.Mono;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityTool : MonoSingleton<ProbabilityTool> , IProbabilityTool
{
    /// <summary>  
    /// ��鵥���¼��Ƿ�����  
    /// </summary>  
    /// <param name="probability">�¼������ĸ��ʣ�Ӧ����0��1֮��ĸ�������</param>  
    /// <returns>�¼��Ƿ�����</returns>  
    public bool CheckProbability(float probability)
    {
        if (!IsValidProbability(probability))
        {
            Debug.LogWarning("����Ӧ��0��1֮�䡣�ṩ��ֵΪ: " + probability);
            return false;
        }

        float randomValue = Random.Range(0f, 1f);
        return randomValue <= probability;
    }

    /// <summary>  
    /// �Ӷ���¼���ѡ����һ���¼���(��ʱû�����ƹ��ܣ�������ʹ��)  
    /// </summary>  
    /// <param name="events">�¼����飬ÿ���¼���һ�����ʡ�</param>  
    /// <returns>�������¼�id�����û���¼�������Ϊ-1��</returns>  
    public int ChooseEvent(EventProbability[] events)
    {
        if (events == null || events.Length == 0)
        {
            Debug.LogWarning("�¼�����Ϊ�ջ�δ���塣");
            return -1;
        }

        float totalProbability = 0f;

        foreach (var ev in events)
        {
            if (!IsValidProbability(ev.probability))
            {
                Debug.LogWarning("�¼� '" + ev.eventName + "' �ĸ�����Ч��Ӧ��0��1֮�䡣");
                return -1;
            }
            totalProbability += ev.probability;
        }

        if (totalProbability <= 0f)
        {
            Debug.LogWarning("�����¼����ܸ���Ӧ����0��");
            return -1;
        }

        float randomValue = Random.Range(0f, totalProbability);
        float cumulativeProbability = 0f;

        foreach (var ev in events)
        {
            cumulativeProbability += ev.probability;
            if (randomValue <= cumulativeProbability)
            {
                return ev.id;
            }
        }

        return -1; // Ӧ�ò����ܵ�������  
    }

    /// <summary>  
    /// ��֤����ֵ�Ƿ�����Ч��Χ�ڡ�  
    /// </summary>  
    /// <param name="probability">����֤�ĸ���ֵ��</param>  
    /// <returns>�Ƿ�Ϊ��Ч�ĸ��ʡ�</returns>  
    private bool IsValidProbability(float probability)
    {
        return probability >= 0f && probability <= 1f;
    }
}

public interface IProbabilityTool
{
    bool CheckProbability(float probability);
    int ChooseEvent(EventProbability[] events);
}

[System.Serializable]
public class EventProbability
{
    public int id;
    public string eventName;
    public float probability;
}
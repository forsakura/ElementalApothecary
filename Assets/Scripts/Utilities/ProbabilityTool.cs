using ProjectBase.Mono;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProbabilityTool : MonoSingleton<ProbabilityTool> , IProbabilityTool
{
    /// <summary>  
    /// 检查单个事件是否发生。  
    /// </summary>  
    /// <param name="probability">事件发生的概率，应该是0到1之间的浮点数。</param>  
    /// <returns>事件是否发生。</returns>  
    public bool CheckProbability(float probability)
    {
        if (!IsValidProbability(probability))
        {
            Debug.LogWarning("概率应在0到1之间。提供的值为: " + probability);
            return false;
        }

        float randomValue = Random.Range(0f, 1f);
        return randomValue <= probability;
    }

    /// <summary>  
    /// 从多个事件中选择发生一个事件。(暂时没有完善功能，不建议使用)  
    /// </summary>  
    /// <param name="events">事件数组，每个事件有一个概率。</param>  
    /// <returns>发生的事件id，如果没有事件发生则为-1。</returns>  
    public int ChooseEvent(EventProbability[] events)
    {
        if (events == null || events.Length == 0)
        {
            Debug.LogWarning("事件数组为空或未定义。");
            return -1;
        }

        float totalProbability = 0f;

        foreach (var ev in events)
        {
            if (!IsValidProbability(ev.probability))
            {
                Debug.LogWarning("事件 '" + ev.eventName + "' 的概率无效，应在0到1之间。");
                return -1;
            }
            totalProbability += ev.probability;
        }

        if (totalProbability <= 0f)
        {
            Debug.LogWarning("所有事件的总概率应大于0。");
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

        return -1; // 应该不可能到达这里  
    }

    /// <summary>  
    /// 验证概率值是否在有效范围内。  
    /// </summary>  
    /// <param name="probability">待验证的概率值。</param>  
    /// <returns>是否为有效的概率。</returns>  
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
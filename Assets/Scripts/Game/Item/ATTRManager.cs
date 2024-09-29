using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本质上就是个buff系统，但主要负责的不是buff的能力
/// </summary>
public class ATTRManager : MonoBehaviour,IATTRManager
{
    private List<BaseAttribute> activeATTR = new List<BaseAttribute>();

    public void Add(BaseAttribute attribute)
    {
        attribute.OnApply(gameObject);
        activeATTR.Add(attribute);

        // 启动协程处理该属性的生命周期  
        StartCoroutine(HandleAttribute(attribute));
    }

    private IEnumerator HandleAttribute(BaseAttribute attribute)
    {
        while (true)
        {
            // 更新属性  
            float deltaTime = Time.deltaTime;
            attribute.OnUpdate(gameObject, deltaTime);

            // 检查属性是否过期  
            if (!attribute.IsPermanent && attribute.IsExpired())
            {
                attribute.OnExpired(gameObject);
                activeATTR.Remove(attribute);
                yield break; // 结束协程  
            }

            // 等待下一帧  
            yield return null;
        }
    }

    /// <summary>  
    /// 处理对外  
    /// </summary>  
    /// <param name="collision"></param>  
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 碰撞处理逻辑  
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 因为实际做的还是更像buff所以也是参考做到，实际功能上并不应该替代buff
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

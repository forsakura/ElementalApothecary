using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour, IBuffManager
{
    private List<BuffBase> activeBuff = new List<BuffBase>();
    public GameObject m_gameObject;

    public void Add(BuffBase buff)
    {
        buff.OnApply(m_gameObject);
        activeBuff.Add(buff);
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        for (int i = activeBuff.Count - 1; i >= 0; i--)
        {
            activeBuff[i].OnUpdate(m_gameObject, deltaTime);
            if (!activeBuff[i].IsPermanent)
            {
                if (activeBuff[i].IsExpired())
                {
                    activeBuff[i].OnExpired(m_gameObject);
                    activeBuff.RemoveAt(i);
                }
            }
        }
    }
}

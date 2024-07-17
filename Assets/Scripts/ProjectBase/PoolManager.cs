using System.Collections.Generic;
using UnityEngine;

namespace ProjectBase
{
    /*
     * ����ع��������������ж���أ���������صĴ�����ɾ������ʼ���������Ϣ���ܡ�      --BY ��
     */
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private Transform parentTransform;
        
        private Dictionary<string, Pool> poolDic = new();

        private void Start()
        {
            
        }

        /// <summary>
        /// ���ӳض���
        /// </summary>
        /// <param name="prefabPath"></param>
        /// <param name="capacity"></param>
        private void AddPoolDic(int capacity, string prefabPath)
        {
            if (poolDic.ContainsKey(prefabPath)) return;
            var gb = new GameObject(prefabPath + " pool");
            gb.transform.SetParent(parentTransform);
            poolDic.Add(prefabPath, new Pool(capacity, prefabPath, gb.transform));
            poolDic[prefabPath].FullPool(null);
            
        }

        /// <summary>
        /// ɾ���ض���
        /// </summary>
        /// <param name="prefabPath"></param>
        private void RmPoolDic(string prefabPath)
        {
            if (!poolDic.ContainsKey(prefabPath)) return;
            poolDic.Remove(prefabPath);
        }

    }
}

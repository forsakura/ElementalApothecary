using System.Collections.Generic;
using ProjectBase.Mono;
using UnityEngine;

namespace ProjectBase.Pool
{
    /*
     * ����ع��������������ж���أ���������صĴ�����ɾ������ʼ���������Ϣ���ܡ�      --BY ��
     */
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private Transform _parentTransform;
        
        private Dictionary<string, ProjectBase.Pool.Pool> poolDic = new();

        private void Start()
        {
            
        }

        /// <summary>
        /// ���ӳض���
        /// </summary>
        /// <param name="prefabPath"></param>
        /// <param name="capacity"></param>
        public void AddPoolDic(int capacity, string prefabPath)
        {
            if (poolDic.ContainsKey(prefabPath)) return;
            var gb = new GameObject(prefabPath + " pool");
            gb.transform.SetParent(_parentTransform);
            poolDic.Add(prefabPath, new ProjectBase.Pool.Pool(capacity, prefabPath, gb.transform));
            poolDic[prefabPath].FullPool(null);
            
        }

        /// <summary>
        /// ɾ���ض���
        /// </summary>
        /// <param name="prefabPath"></param>
        public void RmPoolDic(string prefabPath)
        {
            if (!poolDic.ContainsKey(prefabPath)) return;
            poolDic.Remove(prefabPath);
        }

    }
}

using System.Collections.Generic;
using ProjectBase.Mono;
using UnityEngine;

namespace ProjectBase.Pool
{
    /*
     * 对象池管理器，管理所有对象池，包括对象池的创建与删除，初始化对象池信息功能。      --BY 棾
     */
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private Transform _parentTransform;
        
        private Dictionary<string, ProjectBase.Pool.Pool> poolDic = new();

        private void Start()
        {
            
        }

        /// <summary>
        /// 增加池对象
        /// </summary>
        /// <param name="prefabPath"></param>
        /// <param name="capacity"></param>
        private void AddPoolDic(int capacity, string prefabPath)
        {
            if (poolDic.ContainsKey(prefabPath)) return;
            var gb = new GameObject(prefabPath + " pool");
            gb.transform.SetParent(_parentTransform);
            poolDic.Add(prefabPath, new ProjectBase.Pool.Pool(capacity, prefabPath, gb.transform));
            poolDic[prefabPath].FullPool(null);
            
        }

        /// <summary>
        /// 删除池对象
        /// </summary>
        /// <param name="prefabPath"></param>
        private void RmPoolDic(string prefabPath)
        {
            if (!poolDic.ContainsKey(prefabPath)) return;
            poolDic.Remove(prefabPath);
        }

    }
}

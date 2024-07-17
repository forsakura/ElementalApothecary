using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectBase
{
    /// <summary>
    /// 对象池本身，包括初始化操作，填充对象池，获取和返回物体。                  --By 棾
    /// </summary>
    public class Pool
    {
        private string prefabPath;
        private int initialCapacity = 20;

        private Queue<GameObject> prefabQueue;

        private Transform parent;

        public string PrefabPath
        {
            get => prefabPath;
            set => prefabPath = value;
        }

        public int InitialCapacity
        {
            get => initialCapacity;
            set => initialCapacity = value;
        }

        public Pool(int capacity, string path, Transform parent)
        {
            this.parent = parent;
            InitialCapacity = capacity;
            PrefabPath = path;
            prefabQueue = new Queue<GameObject>();
        }

        /// <summary>
        /// 填充对象池
        /// </summary>
        public void FullPool(UnityAction<GameObject> callback = null)
        {
            for (var i = 0; i < InitialCapacity; i++)
            {
                prefabQueue.Enqueue(InitGameObject(callback));
            }
        }
        
        
        /// <summary>
        /// 创建游戏对象
        /// </summary>
        /// <returns></returns>
        private GameObject InitGameObject(UnityAction<GameObject> callback = null)
        {
            var res = ResManager.LoadResource<GameObject>(PrefabPath);
            res.transform.SetParent(parent);
            callback?.Invoke(res);
            res.SetActive(false);
            return res;
        }

        /// <summary>
        /// 从池子中获取对象
        /// </summary>
        /// <returns></returns>
        private GameObject GetFromPool()
        {
            if (prefabQueue.Count == 0)
            {
                FullPool();
            }

            var obj = prefabQueue.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// 取出物体和返回物体
        /// </summary>
        /// <param name="gb"></param>
        #region API

        public void ReturnToPool(GameObject gb)
        {
            gb.SetActive(false);
            prefabQueue.Enqueue(gb);
        }
        
        public void PrepareGamObject(Vector3 pos)
        {
            var prepareObj = GetFromPool();
            prepareObj.transform.position = pos;
        }

        public void PrepareGameObject(Vector3 pos, Quaternion quaternion)
        {
            var prepareObj = GetFromPool();
            prepareObj.transform.position = pos;
            prepareObj.transform.rotation = quaternion;
        }

        public void PrepareGameObject(Vector3 pos, Quaternion quaternion, Vector3 localScale)
        {
            var prepareObj = GetFromPool();
            prepareObj.transform.position = pos;
            prepareObj.transform.rotation = quaternion;
            prepareObj.transform.localScale = localScale;
        }

        #endregion
    }

}

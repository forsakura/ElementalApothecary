using System.Collections.Generic;
using ProjectBase.Res;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectBase.Pool
{
    /// <summary>
    /// 对象池本身，包括初始化操作，填充对象池，获取和返回物体。                  --By 棾
    /// </summary>
    public class Pool
    {
        private string _prefabPath;
        private int _initialCapacity = 20;

        private Queue<GameObject> _prefabQueue;

        private Transform _parent;

        public string PrefabPath
        {
            get => _prefabPath;
            set => _prefabPath = value;
        }

        public int InitialCapacity
        {
            get => _initialCapacity;
            set => _initialCapacity = value;
        }

        public Pool(int capacity, string path, Transform parent)
        {
            this._parent = parent;
            InitialCapacity = capacity;
            PrefabPath = path;
            _prefabQueue = new Queue<GameObject>();
        }

        /// <summary>
        /// 填充对象池
        /// </summary>
        public void FullPool(UnityAction<GameObject> callback = null)
        {
            for (var i = 0; i < InitialCapacity; i++)
            {
                InitGameObject(obj => _prefabQueue.Enqueue(obj));
            }
        }
        
        
        /// <summary>
        /// 创建游戏对象
        /// </summary>
        /// <returns></returns>
        private void InitGameObject(UnityAction<GameObject> callback = null)
        {
            ResManager.LoadResourceAsync<GameObject>(PrefabPath, obj =>
            {
                obj.transform.SetParent(_parent);
                callback?.Invoke(obj);
                obj.SetActive(false);
            });
        }

        /// <summary>
        /// 从池子中获取对象
        /// </summary>
        /// <returns></returns>
        private GameObject GetFromPool()
        {
            if (_prefabQueue.Count == 0)
            {
                FullPool();
            }

            var obj = _prefabQueue.Dequeue();
            obj.transform.parent = null;
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
            gb.transform.SetParent(_parent);
            _prefabQueue.Enqueue(gb);
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

using System.Collections.Generic;
using ProjectBase.Res;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectBase.Pool
{
    /// <summary>
    /// ����ر���������ʼ��������������أ���ȡ�ͷ������塣                  --By ��
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
        /// �������
        /// </summary>
        public void FullPool(UnityAction<GameObject> callback = null)
        {
            for (var i = 0; i < InitialCapacity; i++)
            {
                InitGameObject(obj => _prefabQueue.Enqueue(obj));
            }
        }
        
        
        /// <summary>
        /// ������Ϸ����
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
        /// �ӳ����л�ȡ����
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
        /// ȡ������ͷ�������
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

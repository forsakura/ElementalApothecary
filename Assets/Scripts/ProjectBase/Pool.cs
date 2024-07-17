using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectBase
{
    /// <summary>
    /// ����ر���������ʼ��������������أ���ȡ�ͷ������塣                  --By ��
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
        /// �������
        /// </summary>
        public void FullPool(UnityAction<GameObject> callback = null)
        {
            for (var i = 0; i < InitialCapacity; i++)
            {
                prefabQueue.Enqueue(InitGameObject(callback));
            }
        }
        
        
        /// <summary>
        /// ������Ϸ����
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
        /// �ӳ����л�ȡ����
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
        /// ȡ������ͷ�������
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

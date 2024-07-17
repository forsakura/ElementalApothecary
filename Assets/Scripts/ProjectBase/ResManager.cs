using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectBase
{
    /*
     * 资源管理器，对资源的进行加载和卸载操作，提供同步和异步操作。
     * Resources文件夹下资源进行处理
     * AssetBundle后续补充          --by 棾
     */
    public static class ResManager
    {
        #region Resources

        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T LoadResource<T>(string path) where T : Object
        {
            var res = Resources.Load<T>(path);
            return res is GameObject ? Object.Instantiate(res) : res;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <param name="path"></param>
        /// <param name="callback"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void LoadResourceAsync<T>(string path, UnityAction<T> callback) where T : Object
        {
            MonoBehaviourController.Instance.StartCoroutine(IELoadResourceAsync(path, callback));
        }

        private static IEnumerator IELoadResourceAsync<T>(string path, UnityAction<T> callback) where T : Object
        {
            var res = Resources.LoadAsync<T>(path);
            yield return res;
            if (res.asset is GameObject)
            {
                Object.Instantiate(res.asset);
            }
            else
            {
                callback(res.asset as T);
            }
        }

        #endregion

        #region AssetBundle

        

        #endregion
        
    }
}

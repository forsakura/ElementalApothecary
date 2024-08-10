using System;
using System.Collections;
using ProjectBase.Event;
using ProjectBase.Mono;
using ProjectBase.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace ProjectBase.Scene
{
    /*
     * 场景管理类，主要实现场景加载，场景卸载    --BY 棾
     */
    public class SceneMgr : SingletonByQing<SceneMgr>
    {
        /// <summary>
        /// 同步加载场景
        /// </summary>
        /// <param name="sceneName">场景名</param>
        /// <param name="isAddType">是否叠加</param>
        /// <param name="callback">回调方法</param>
        public void LoadScene(string sceneName, bool isAddType, UnityAction callback)
        {
            SceneManager.LoadScene(sceneName, isAddType ? LoadSceneMode.Additive : LoadSceneMode.Single);
            GC.Collect();
            callback?.Invoke();
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneName">场景名</param>
        /// <param name="isAddType">是否叠加</param>
        /// <param name="callback">回调方法</param>
        public void LoadSceneAsync(string sceneName, bool isAddType, UnityAction callback)
        {
            MonoBehaviourController.Instance.StartCoroutine(IELoadScene(sceneName, isAddType, callback));
        }
        
        IEnumerator IELoadScene(string sceneName, bool isAddType, UnityAction callback)
        {
            UIManager.Instance.ShowPanel<BasePanel>("loadingPanel", E_UI_Layer.top);
            var res = SceneManager.LoadSceneAsync(sceneName, isAddType ? LoadSceneMode.Additive : LoadSceneMode.Single);
            while (!res.isDone)
            {
                EventCenter.Instance.EventTrigger("进度更新", res.progress);
                yield return res.progress;
            }
            GC.Collect();
            UIManager.Instance.HidePanel("loadingPanel");
            callback?.Invoke();
        }


        /// <summary>
        /// 异步卸载场景，bool判断是否卸载当前场景中所有的物体，默认为卸载所有
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="callback"></param>
        /// <param name="isAllObjectUnload"></param>
        public void UnloadSceneAsync(string sceneName, UnityAction callback, bool isAllObjectUnload = false)
        {
            MonoBehaviourController.Instance.StartCoroutine(IEUnloadSceneAsync(sceneName, callback, isAllObjectUnload));
        }
        private IEnumerator IEUnloadSceneAsync(string sceneName,UnityAction callback, bool isAllObjectUnload = false)
        {
            AsyncOperation asyncOperation = SceneManager.UnloadSceneAsync(sceneName,
                isAllObjectUnload ? UnloadSceneOptions.UnloadAllEmbeddedSceneObjects : UnloadSceneOptions.None);
            while (!asyncOperation.isDone)
            {
                EventCenter.Instance.EventTrigger("卸载进度", asyncOperation.progress);
                yield return asyncOperation.progress;
            }
            callback?.Invoke();
        }
    }
}

using System.Collections;
using ProjectBase.Event;
using ProjectBase.Mono;
using ProjectBase.UI;
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
            UIManager.Instance.HidePanel("loadingPanel");
            callback?.Invoke();
        }
    }
}

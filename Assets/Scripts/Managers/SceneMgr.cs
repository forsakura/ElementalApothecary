using System.Collections;
using ProjectBase;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Managers
{
    /*
     * 场景管理类，主要实现场景加载，场景卸载
     */
    public class SceneMgr : MonoSingleton<SceneMgr>
    {
        public void LoadScene(string sceneName, bool isAddType)
        {
            SceneManager.LoadScene(sceneName, isAddType ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }

        IEnumerator IELoadScene(string sceneName, bool isAddType, UnityAction callback)
        {
            UIManager.Instance.loadingPanel.SetActive(true);
            var res = SceneManager.LoadSceneAsync(sceneName, isAddType ? LoadSceneMode.Additive : LoadSceneMode.Single);
            yield return res;
            UIManager.Instance.loadingPanel.SetActive(false);
            callback?.Invoke();
        }
    }
}

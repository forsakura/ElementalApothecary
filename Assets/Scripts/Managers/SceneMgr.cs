using System.Collections;
using System.Collections.Generic;
using FrameWork;
using FrameWork.Base;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

/// <summary>
/// 场景管理类
/// </summary>
public class SceneMgr : MonoSingleton<SceneMgr>
{
    
    /*
    private AssetBundle loadedAssetBundle;
    public string[] scenePaths;

    private Dictionary<int, Scene> sceneDic = new Dictionary<int, Scene>();*/

    private void Awake()
    {
        
    }
    private void OnEnable()
    {
        DontDestroyOnLoad(Instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        // loadedAssetBundle = AssetBundle.LoadFromFile("Assets/Scenes/Menu");
        // scenePaths = loadedAssetBundle.GetAllScenePaths();
    }

    /// <summary>
    /// 按场景名加载场景
    /// </summary>
    /// <param name="sceneName">场景名</param>
    public void LoadNextScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    /// <summary>
    /// 协程 加载下一个场景，按场景名加载场景
    /// </summary>
    /// <param name="sceneName">场景名</param>
    /// <returns></returns>
    private static IEnumerator LoadSceneAsync(string sceneName)
    {
        UIManager.Instance.loadingPanel.SetActive(true);
        var asyncOperation = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
        while (!asyncOperation.isDone)
        {
            yield return null;
            if (asyncOperation.progress >= 0.9)
            {
                UIManager.Instance.loadingPanel.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 按场景名卸载场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void UnLoadSceneAsync(string sceneName)
    {
        StartCoroutine(UnloadSceneAsync(sceneName));
    }
    
    private IEnumerator UnloadSceneAsync(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (!scene.isLoaded)
        {
            yield break;
        }

        AsyncOperation async = SceneManager.UnloadSceneAsync(scene);
        yield return async;
    }
}

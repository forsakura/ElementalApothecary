using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<string , string, Vector3> LoadSceneEvent;

   /// <summary>
   /// 场景加载
   /// </summary>
   /// <param name="from">当前场景</param>
   /// <param name="to">要去场景</param>
   /// <param name="posToGo">要去坐标</param>
   /// <param name="fadeScreen">是否淡入淡出</param>
    public void RaiseLoadScenetEvent(string from, string to, Vector3 posToGo)
    {
        LoadSceneEvent?.Invoke(from, to,posToGo);
    }
}

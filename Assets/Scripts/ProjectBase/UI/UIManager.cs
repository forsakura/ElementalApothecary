using System.Collections.Generic;
using ProjectBase.Mono;
using ProjectBase.Res;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace ProjectBase.UI
{
    /*
     * 管理所有显示的面板
     *      请将所有面板预制体放置在Resources/UI文件夹下。
     *      提供显示，隐藏和获取面板的功能。             注意：显示和隐藏的方式不是SetActive而是加载和卸载
     */

    public enum E_UI_Layer
    {
        top,
        mid,
        down,
        system
    }
    public class UIManager : SingletonByQing<UIManager>
    {
        public Dictionary<string, BasePanel> panelsDic = new Dictionary<string, BasePanel>();

        public RectTransform canvas;
        
        private Transform top;
        private Transform mid;
        private Transform down;
        private Transform system;

        public UIManager()
        {
            var obj = ResManager.LoadResource<GameObject>("UI/Canvas");
            canvas = obj.transform as RectTransform;
            top = canvas.Find("Top");
            top = canvas.Find("Mid");
            top = canvas.Find("Down");
            top = canvas.Find("System");
            Object.DontDestroyOnLoad(obj);
            obj = ResManager.LoadResource<GameObject>("UI/EventSystem");
            Object.DontDestroyOnLoad(obj);
        }

        /// <summary>
        /// 通过层级对象得到对应层级父对象
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Transform GetFatherLayer(E_UI_Layer layer)
        {
            switch (layer)
            {
                case E_UI_Layer.top:
                    return top;
                case E_UI_Layer.mid:
                    return mid;
                case E_UI_Layer.down:
                    return down;
                case E_UI_Layer.system:
                    return system;
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// 显示面板
        /// </summary>
        /// <param name="panelName">需要显示的面板名</param>
        /// <param name="layer">显示在哪一层</param>
        /// <param name="callback">面板显示完成后，需要对该面板做的事，默认置空</param>
        /// <typeparam name="T"></typeparam>
        public void ShowPanel<T>(string panelName, E_UI_Layer layer, UnityAction<T> callback = null) where T : BasePanel
        {
            if (panelsDic.TryGetValue(panelName, out var value))
            {
                panelsDic[panelName].ShowMe();
                callback?.Invoke(value as T);
                return;
            }
            ResManager.LoadResourceAsync<GameObject>("UI/" + panelName, (obj) =>
            {
                var father = down;
                switch (layer)
                {
                    case E_UI_Layer.top : father = top;
                        break;
                    case E_UI_Layer.mid : father = mid;
                        break;
                    case E_UI_Layer.system : father = system;
                        break;
                    default :
                        break;
                }
                obj.transform.SetParent(father);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale=Vector3.one;
                ((RectTransform)obj.transform).offsetMax = Vector2.zero;
                ((RectTransform)obj.transform).offsetMin = Vector2.zero;

                var panel = obj.GetComponent<T>();
                panel.ShowMe();
                callback?.Invoke(panel);
                panelsDic.Add(panelName, panel);
            });
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="panelName">需要隐藏的面板名</param>
        public void HidePanel(string panelName)
        {
            if (panelsDic[panelName] == null) return;
            panelsDic[panelName].HideMe();
            Object.Destroy(panelsDic[panelName].gameObject);
            panelsDic.Remove(panelName);
        }

        /// <summary>
        /// 获取已加载的面板
        /// </summary>
        /// <param name="panelName">面板名称</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetPanel<T>(string panelName) where T : BasePanel
        {
            return panelsDic.TryGetValue(panelName, out BasePanel value) ? (T)value : null;
        }

        /// <summary>
        /// 给控件对象添加自定义事件
        /// </summary>
        /// <param name="control">控件对象</param>
        /// <param name="type">事件类型</param>
        /// <param name="callback">事件响应函数</param>
        public static void AddCustomEvent(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callback)
        {
            var eventTrigger = control.GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = control.AddComponent<EventTrigger>();
            }

            var entry = new EventTrigger.Entry
            {
                eventID = type
            };
            entry.callback.AddListener(callback);
            
            eventTrigger.triggers.Add(entry);
        }
    }
}

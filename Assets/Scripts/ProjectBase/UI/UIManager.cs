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
     * ����������ʾ�����
     *      �뽫�������Ԥ���������Resources/UI�ļ����¡�
     *      �ṩ��ʾ�����غͻ�ȡ���Ĺ��ܡ�             ע�⣺��ʾ�����صķ�ʽ����SetActive���Ǽ��غ�ж��
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
        /// ͨ���㼶����õ���Ӧ�㼶������
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
        /// ��ʾ���
        /// </summary>
        /// <param name="panelName">��Ҫ��ʾ�������</param>
        /// <param name="layer">��ʾ����һ��</param>
        /// <param name="callback">�����ʾ��ɺ���Ҫ�Ը���������£�Ĭ���ÿ�</param>
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
        /// �������
        /// </summary>
        /// <param name="panelName">��Ҫ���ص������</param>
        public void HidePanel(string panelName)
        {
            if (panelsDic[panelName] == null) return;
            panelsDic[panelName].HideMe();
            Object.Destroy(panelsDic[panelName].gameObject);
            panelsDic.Remove(panelName);
        }

        /// <summary>
        /// ��ȡ�Ѽ��ص����
        /// </summary>
        /// <param name="panelName">�������</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetPanel<T>(string panelName) where T : BasePanel
        {
            return panelsDic.TryGetValue(panelName, out BasePanel value) ? (T)value : null;
        }

        /// <summary>
        /// ���ؼ���������Զ����¼�
        /// </summary>
        /// <param name="control">�ؼ�����</param>
        /// <param name="type">�¼�����</param>
        /// <param name="callback">�¼���Ӧ����</param>
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

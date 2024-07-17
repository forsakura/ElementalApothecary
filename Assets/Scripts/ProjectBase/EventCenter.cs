using System.Collections.Generic;
using UnityEngine.Events;

namespace ProjectBase
{
    /*
     * �¼����ģ����д����¼������������¼�֮����϶ȡ�    --By ��
     */
    
    public interface IEventInfo
    {
        
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;
        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }

    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;
        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }
    public class EventCenter : SingletonByQing<EventCenter>
    {
        private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();
        
        /// <summary>
        /// ����¼�����
        /// </summary>
        /// <param name="eventName">���������¼�����</param>
        /// <param name="action">�������¼���Ϊ</param>
        public void AddEventListener(string eventName, UnityAction action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo).actions += action;
            }
            else
            {
                eventDic.Add(eventName, new EventInfo(action));
            }
        }

        public void AddEventLister<T>(string eventName, UnityAction<T> action)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo<T>).actions += action;
            }
            else
            {
                eventDic.Add(eventName, new EventInfo<T>(action));
            }
        }

        /// <summary>
        /// �¼�������
        /// </summary>
        /// <param name="eventName">�����ı������¼�����</param>
        public void EventTrigger(string eventName)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo).actions.Invoke();
        }

        public void EventTrigger<T>(string eventName, T info)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo<T>).actions.Invoke(info);
        }

        /// <summary>
        /// �Ƴ��¼�����
        /// </summary>
        /// <param name="eventName">���������¼�����</param>
        /// <param name="action">�������¼���Ϊ</param>
        public void RemoveEventLister(string eventName, UnityAction action)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo).actions -= action;
        }

        public void RemoveEventLister<T>(string eventName, UnityAction<T> action)
        {
            if (!eventDic.ContainsKey(eventName)) return;
            (eventDic[eventName] as EventInfo<T>).actions -= action;
        }

        /// <summary>
        /// �Ƴ������¼�����
        /// </summary>
        public void RemoveAllLister()
        {
            eventDic.Clear();
        }
    }
}

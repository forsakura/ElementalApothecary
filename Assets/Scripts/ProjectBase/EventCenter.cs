using System.Collections.Generic;
using UnityEngine.Events;

namespace ProjectBase
{
    /*
     * 事件中心，集中处理事件，减少物体事件之间耦合度。    --By 棾
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
        /// 添加事件监听
        /// </summary>
        /// <param name="eventName">被监听的事件名称</param>
        /// <param name="action">触发的事件行为</param>
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
        /// 事件触发器
        /// </summary>
        /// <param name="eventName">触发的被监听事件名称</param>
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
        /// 移除事件监听
        /// </summary>
        /// <param name="eventName">被监听的事件名称</param>
        /// <param name="action">触发的事件行为</param>
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
        /// 移除所有事件监听
        /// </summary>
        public void RemoveAllLister()
        {
            eventDic.Clear();
        }
    }
}

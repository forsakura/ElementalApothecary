using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace FrameWork
{
    public class MonoBehaviourController : MonoSingleton<MonoBehaviourController>
    {
        private UnityAction updateUnityAction;

        private List<UnityAction> updateUnityActionList;

        protected override void Init()
        {
            base.Init();
            updateUnityActionList = new List<UnityAction>();
        }

        private void Update()
        {
            updateUnityAction?.Invoke();
        }

        /// <summary>
        /// 添加指定更新事件
        /// </summary>
        /// <param name="action"></param>
        public void AddUpdateAction(UnityAction action)
        {
            updateUnityAction += action;
            updateUnityActionList.Add(action);
        }

        /// <summary>
        /// 删除指定更新事件
        /// </summary>
        /// <param name="action"></param>
        public void DelUpdateAction(UnityAction action)
        {
            updateUnityAction -= action;
            updateUnityActionList.Remove(action);
        }

        /// <summary>
        /// 删除所有更新事件
        /// </summary>
        public void DelAllUpdateActions()
        {
            foreach (var unityAction in updateUnityActionList)
            {
                DelUpdateAction(unityAction);
            }
        }
    }
}

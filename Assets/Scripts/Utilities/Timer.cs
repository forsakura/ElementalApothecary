using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using MonoBehaviourController = FrameWork.MonoBehaviourController;

namespace Utilities
{
    public class Timer
    {
        #region Actions

        private UnityAction enterAction;
        private UnityAction intervalAction;
        private UnityAction endAction;

        #endregion

        #region parameter
        
        private float curTime;
        private float totalTime;
        
        private float curIntervalTime;
        private float intervalTime;

        private bool isPause;
        private bool isActive;
        private bool enterActionActive;

        #endregion

        public Timer(float totalTime, float intervalTime, UnityAction enterAction, UnityAction intervalAction,
            UnityAction endAction, bool isPause = false, bool isActive = true)
        {
            this.totalTime = totalTime;
            this.intervalTime = intervalTime;
            this.enterAction = enterAction;
            this.intervalAction = intervalAction;
            this.endAction = endAction;
            this.isPause = isPause;
            this.isActive = isActive;
            curTime = 0;
            curIntervalTime = 0;
            MonoBehaviourController.Instance.AddUpdateAction(Timing);
        }

        /// <summary>
        /// 计时
        /// </summary>
        void Timing()
        {
            if (isPause || !isActive) return;
            if (!enterActionActive)
            {
                enterAction?.Invoke();
                enterActionActive = true;
            }
            if (curTime < totalTime)
            {
                curTime += Time.deltaTime;
                curIntervalTime += Time.deltaTime;
            }

            if (curIntervalTime > intervalTime)
            {
                intervalAction?.Invoke();
                curIntervalTime = 0;
            }

            if (curTime >= totalTime)
            {
                isActive = false;
                curTime = 0;
                endAction?.Invoke();
            }
            
        }

        /// <summary>
        /// 倒计时重置
        /// </summary>
        public void ResetTime()
        {
            curTime = 0;
            curIntervalTime = 0;
            isActive = true;
            isPause = false;
            enterActionActive = false;
        }

        /// <summary>
        /// 暂停or继续
        /// </summary>
        public void PauseTime()
        {
            isPause = !isPause;
        }
        
        /// <summary>
        /// 设置间隔时间及事件
        /// </summary>
        /// <param name="intervalTime"></param>
        /// <param name="intervalAction"></param>
        public void SetInterval(float intervalTime, UnityAction intervalAction)
        {
            this.intervalTime = intervalTime;
            this.intervalAction = intervalAction;
        }
            
    }
}

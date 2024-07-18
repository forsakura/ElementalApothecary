using ProjectBase.Event;
using ProjectBase.Mono;
using UnityEngine;

namespace ProjectBase.Input
{
    /*
     * 输入控制模块，配合事件中心使用，在该模块中添加需要检测的按键输入，并通过事件中心分发事件，监听事件并触发在其他脚本编写。
     * 包含是否开启输入检测，检测某按键按下抬起功能
     */
    public class InputMgr : SingletonByQing<InputMgr>
    {
        private bool _isOpen = false;
        public InputMgr()
        {
            MonoBehaviourController.Instance.AddUpdateAction(InputUpdate);
        }

        /// <summary>
        /// 是否开启或关闭输入检测
        /// </summary>
        /// <param name="isOpen"></param>
        public void StartOrEndCheck(bool isOpen)
        {
            _isOpen = isOpen;
        }

        public void ChangeKeyCode(KeyCode key)
        {
            
        }
        
        /// <summary>
        /// 检测某个按键按下抬起，并触发事件
        /// </summary>
        /// <param name="key"></param>
        public void CheckKeyCode(KeyCode key)
        {
            if (UnityEngine.Input.GetKeyDown(key))
            {
                EventCenter.Instance.EventTrigger("某键按下", key);
            }
            
            else if (UnityEngine.Input.GetKey(key))
            {
                EventCenter.Instance.EventTrigger("某键持续按下", key);
            }

            if (UnityEngine.Input.GetKeyUp(key))
            {
                EventCenter.Instance.EventTrigger("某键抬起", key);
            }
        }

        /// <summary>
        /// 需要检测的按键事件加入该事件中。
        /// </summary>
        public void InputUpdate()
        {
            if(!_isOpen) return;
            //CheckKeyCode(KeyCode.W);
            //CheckKeyCode(KeyCode.A);
            //CheckKeyCode(KeyCode.S);
            //CheckKeyCode(KeyCode.D);
        }
    
    }
}

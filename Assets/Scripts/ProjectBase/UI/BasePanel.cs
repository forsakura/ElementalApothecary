using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectBase.UI
{
    /*
     * 面板基类，所有面板可继承此面板实现以下功能：
     *                  直接获取某个UI控件，例如Button，Image，Text，Toggle，Slider，ScrollRect，如需添加其他组件直接在Awake（）添加。
     *                  提供隐藏和显示的方法；
     *                                                              --BY 棾
     */
    public class BasePanel : MonoBehaviour
    {
        private Dictionary<string, List<UIBehaviour>> _uiControlDic = new Dictionary<string, List<UIBehaviour>>();

        protected virtual void Awake()
        {
            FindChildrenComponent<Button>();
            FindChildrenComponent<Image>();
            FindChildrenComponent<Text>();
            FindChildrenComponent<Toggle>();
            FindChildrenComponent<Slider>();
            FindChildrenComponent<ScrollRect>();
            FindChildrenComponent<InputField>();
        }

        /// <summary>
        /// 展示本面板，具体看该面板
        /// </summary>
        public virtual void ShowMe()
        {
            
        }

        /// <summary>
        /// 隐藏本面板，具体看该面板
        /// </summary>
        public virtual void HideMe()
        {
            
        }

        /*
         * 组件监听事件
         */
        #region components

        /// <summary>
        /// 处理按钮点击事件
        /// 该面板的子物体的按钮组件被点击时需要触发的事件
        /// </summary>
        /// <param name="buttonName"></param>
        public virtual void OnClick(string buttonName)
        {
            
        }

        /// <summary>
        /// 处理勾选框事件
        /// 该面板下子物体的勾选框组件被点击时需要触发的事件
        /// </summary>
        /// <param name="toggleName"></param>
        /// <param name="value"></param>
        public virtual void OnToggleValueChanged(string toggleName, bool value)
        {
            
        }

        /// <summary>
        /// 处理slider组件值变化时触发事件
        /// </summary>
        /// <param name="sliderName"></param>
        /// <param name="value"></param>
        public virtual void OnSliderValueChanged(string sliderName, float value)
        {
            
        }

        /// <summary>
        /// 处理文本输入框内容发生变化时触发的事件
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="value"></param>
        public virtual void OnInputFieldValueChanged(string inputName, string value)
        {
            
        }

        #endregion
        
        /// <summary>
        /// 获取某个特定组件
        /// </summary>
        /// <param name="controlName">该面板下指定子物体的名称，从该物体获取控件</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetControl<T>(string controlName) where T : UIBehaviour
        {
            if (!_uiControlDic.ContainsKey(controlName)) return null;
            foreach (var uiBehaviour in _uiControlDic[controlName])
            {
                if (uiBehaviour is T behaviour)
                {
                    return behaviour;
                }
            }

            return null;
        }
        
        /// <summary>
        /// 获取该面板子物体的控件组件并添加监听事件
        /// </summary>
        /// <typeparam name="T">控件类型</typeparam>
        protected void FindChildrenComponent<T>() where T : UIBehaviour
        {
            var components = this.GetComponentsInChildren<T>();
            foreach (var component in components)
            {
                var objectName = component.gameObject.name;
                if (_uiControlDic.ContainsKey(objectName))
                {
                    _uiControlDic[objectName].Add(component);
                }
                else
                {
                    _uiControlDic.Add(objectName, new List<UIBehaviour>() { component });
                }

                switch (component)
                {
                    case Button button:
                        button.onClick.AddListener(() =>
                        {
                            OnClick(objectName);
                        });
                        break;
                    case Toggle toggle:
                        toggle.onValueChanged.AddListener((value) =>
                        {
                            OnToggleValueChanged(objectName, value);
                        });
                        break;
                    case Slider slider:
                        slider.onValueChanged.AddListener((value) =>
                        {
                            OnSliderValueChanged(objectName, value);
                        });
                        break;
                    case InputField inputField:
                        inputField.onValueChanged.AddListener((value) =>
                        {
                            OnInputFieldValueChanged(objectName, value);
                        });
                        break;
                }
            }
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ProjectBase.UI
{
    /*
     * �����࣬�������ɼ̳д����ʵ�����¹��ܣ�
     *                  ֱ�ӻ�ȡĳ��UI�ؼ�������Button��Image��Text��Toggle��Slider��ScrollRect����������������ֱ����Awake������ӡ�
     *                  �ṩ���غ���ʾ�ķ�����
     *                                                              --BY ��
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
        /// չʾ����壬���忴�����
        /// </summary>
        public virtual void ShowMe()
        {
            
        }

        /// <summary>
        /// ���ر���壬���忴�����
        /// </summary>
        public virtual void HideMe()
        {
            
        }

        /*
         * ��������¼�
         */
        #region components

        /// <summary>
        /// ����ť����¼�
        /// ������������İ�ť��������ʱ��Ҫ�������¼�
        /// </summary>
        /// <param name="buttonName"></param>
        public virtual void OnClick(string buttonName)
        {
            
        }

        /// <summary>
        /// ����ѡ���¼�
        /// �������������Ĺ�ѡ����������ʱ��Ҫ�������¼�
        /// </summary>
        /// <param name="toggleName"></param>
        /// <param name="value"></param>
        public virtual void OnToggleValueChanged(string toggleName, bool value)
        {
            
        }

        /// <summary>
        /// ����slider���ֵ�仯ʱ�����¼�
        /// </summary>
        /// <param name="sliderName"></param>
        /// <param name="value"></param>
        public virtual void OnSliderValueChanged(string sliderName, float value)
        {
            
        }

        /// <summary>
        /// �����ı���������ݷ����仯ʱ�������¼�
        /// </summary>
        /// <param name="inputName"></param>
        /// <param name="value"></param>
        public virtual void OnInputFieldValueChanged(string inputName, string value)
        {
            
        }

        #endregion
        
        /// <summary>
        /// ��ȡĳ���ض����
        /// </summary>
        /// <param name="controlName">�������ָ������������ƣ��Ӹ������ȡ�ؼ�</param>
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
        /// ��ȡ�����������Ŀؼ��������Ӽ����¼�
        /// </summary>
        /// <typeparam name="T">�ؼ�����</typeparam>
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

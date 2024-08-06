using ProjectBase.Event;
using ProjectBase.Mono;
using UnityEngine;

namespace ProjectBase.Input
{
    /*
     * �������ģ�飬����¼�����ʹ�ã��ڸ�ģ���������Ҫ���İ������룬��ͨ���¼����ķַ��¼��������¼��������������ű���д��
     * �����Ƿ��������⣬���ĳ��������̧����
     */
    public class InputMgr : SingletonByQing<InputMgr>
    {
        private bool _isOpen = false;
        public InputMgr()
        {
            MonoBehaviourController.Instance.AddUpdateAction(InputUpdate);
        }

        /// <summary>
        /// �Ƿ�����ر�������
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
        /// ���ĳ����������̧�𣬲������¼�
        /// </summary>
        /// <param name="key"></param>
        public void CheckKeyCode(KeyCode key)
        {
            if (UnityEngine.Input.GetKeyDown(key))
            {
                EventCenter.Instance.EventTrigger("ĳ������", key);
            }
            
            else if (UnityEngine.Input.GetKey(key))
            {
                EventCenter.Instance.EventTrigger("ĳ����������", key);
            }

            if (UnityEngine.Input.GetKeyUp(key))
            {
                EventCenter.Instance.EventTrigger("ĳ��̧��", key);
            }
        }

        /// <summary>
        /// ��Ҫ���İ����¼�������¼��С�
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

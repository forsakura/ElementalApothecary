using Unity.VisualScripting;
using UnityEngine;

namespace FrameWork.Base
{
    /*
     * 不继承Mono的脚本单例类
     */
    public class Singleton<T> where T : Singleton<T>, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                    _instance.Init();
                }

                return _instance;
            }
        }

        protected virtual void Init()
        {
            
        }
    }
}

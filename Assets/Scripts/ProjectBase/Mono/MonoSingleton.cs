using UnityEngine;

namespace ProjectBase.Mono
{
    /*
     * 继承Mono的脚本单例类                    --By 棾
     */ 
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;
                _instance = new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
                _instance.Init();

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null) return;
            _instance = this as T;
            Init();
        }

        protected virtual void Init()
        {
            
        }
    }
}

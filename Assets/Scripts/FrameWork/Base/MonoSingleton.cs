using UnityEngine;

namespace FrameWork
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = FindObjectOfType<T>();
                if (instance != null) return instance;
                instance = new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
                instance.Init();

                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null) return;
            instance = this as T;
            Init();
        }

        protected virtual void Init()
        {
            
        }
    }
}

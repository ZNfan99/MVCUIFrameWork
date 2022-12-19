using UnityEngine;

namespace MVC2
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
    	private static T mInstance = null;
    
    	public static T Instance
        {
            get
            {
    			if (mInstance == null)
                {
                    //我担心你在场景里有一个对象了，我先找一下
                	mInstance = GameObject.FindObjectOfType<T>() as T;
                    if (mInstance == null)
                    {
                        GameObject go = new GameObject(typeof(T).Name);
                        mInstance = go.AddComponent<T>();
                        GameObject parent = GameObject.Find("Boot");
                        if (parent == null)
                        {
                            parent = new GameObject("Boot");
                            // 让该游戏对象，场景切换时，不会被销毁
                            GameObject.DontDestroyOnLoad(parent);
                        }
                        if (parent != null)
                        {
                            go.transform.parent = parent.transform;
                        }
                    }
                }
    
                return mInstance;
            }
        }
    
   
        public virtual void Startup()
        {
            
        }
    
        private void Awake()
        {
            if (mInstance == null)
            {
                mInstance = this as T;
            }
    
            DontDestroyOnLoad(gameObject);
            Init();
        }
     
        protected virtual void Init()
        {
    
        }
    
        public void DestroySelf()
        {
            Dispose();
            MonoSingleton<T>.mInstance = null;
            UnityEngine.Object.Destroy(gameObject);
        }
    
        public virtual void Dispose()
        {
    
        }
    
    }
}

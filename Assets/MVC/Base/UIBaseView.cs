
using UnityEngine;

namespace MVC2
{
    public abstract class UIBaseView : MonoBehaviour
    {

        // public bool isActivating;
        // public bool isUnActivating;
        public virtual bool IsActive => gameObject.activeSelf;

        /// <summary>
        ///  显示
        /// </summary>
        public virtual void Active()
        {
            gameObject.SetActive(true);
            OnActiveComplete();
        }
        /// <summary>
        ///  当显示成功后，调用
        /// </summary>
        public virtual void OnActiveComplete()
        {
            
        }

        public virtual void UnActive()
        {
            gameObject.SetActive(false);
            OnUnActiveComplete();
        }
        /// <summary>
        /// 当关闭成功后调用
        /// </summary>
        public virtual void OnUnActiveComplete()
        {
            
        }
    }
}
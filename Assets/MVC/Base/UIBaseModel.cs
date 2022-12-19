
using UnityEngine;

namespace MVC2
{
    public abstract class UIBaseModel : MonoBehaviour
    {
        
        public virtual void Active()
        {
            enabled = true;
        }
        public virtual void UnActive()
        {
            enabled = false;
        }

    }
}
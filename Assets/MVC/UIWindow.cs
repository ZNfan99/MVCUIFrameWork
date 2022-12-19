
using UnityEngine;

namespace MVC2
{
    /// <summary>
    /// 表示 一个窗口
    /// </summary>
    public class UIWindow
    {
        public UiWindowName windowName;
        
        private UIBaseView m_BaseView;

        public UIBaseView MBaseView => m_BaseView;

        private UIBaseModel m_BaseModel;

        public UIBaseModel MBaseModel => m_BaseModel;

        public GameObject gameObject;
        
        public UIWindow(UiWindowName windowName,  UIBaseView view,UIBaseModel model)
        {
            this.windowName = windowName;
            if (view != null && model != null)
            {
                this.m_BaseModel = model;
                this.m_BaseView = view;
            
                gameObject = view.gameObject;
            }
            
        }
        
        
        public void Active()
        {
            // m_BaseModel.enabled = true;
            // m_BaseView.enabled = true;
            m_BaseView.Active();
            m_BaseModel.Active();
            // gameObject.SetActive(true);
        }

        public void UnActive()
        {
            m_BaseView.UnActive();
            m_BaseModel.UnActive();
            // m_BaseModel.enabled = true;
            // m_BaseView.enabled = false;
            // gameObject.SetActive(false);
        }
        
    }
}
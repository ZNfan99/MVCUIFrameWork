using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace MVC2
{
    [Flags]
    public enum UiWindowName
    {
        BagView = 1,
        MainView = 2,
        Repository = 4,
    }

    public class UIManager : MonoBehaviour
    {
        public GameObject prefabUINotification;
        private static UIManager _uiManager;
        private RectTransform m_UIRoot;
        //做模态遮挡用的
        private RectTransform m_RtMaskPanel;
        
        

        // public Canvas canvas;
        public List<Canvas> canvasList = new List<Canvas>();
        public static UIManager Instance => _uiManager;
        //SAO
        public UIWindowConfigs uiWindowConfigs;

        private readonly Dictionary<UiWindowName, UIWindow> m_UIWindows = new Dictionary<UiWindowName, UIWindow>();


        // private readonly List<UIWindow> _openedWindows = new List<UIWindow>();

        private Canvas GetBelongCanvas(SortingLayer sortingLayer)
        {
            return GetBelongCanvas((int)sortingLayer);
        }

        private Canvas GetBelongCanvas(int index)
        {
            return canvasList[index];
        }

        public UIWindow GetWindow(UiWindowName windowName)
        {
            
            // 加载这个窗口对象并打开
            //先找配置文件
          
            if (m_UIWindows.TryGetValue(windowName, out var window))
            {
               
            }
            else
            {
                var config = uiWindowConfigs.GetWindowConfig(windowName);
                var windowObj = GameObject.Instantiate(config.prefab, 
                    GetBelongCanvas(config.sortingLayer).transform,false);

                window = new UIWindow(windowName, windowObj.GetComponent<UIBaseView>(),
                    windowObj.GetComponent<UIBaseModel>());
                //不要激活窗口对象任何生命函数
                window.gameObject.SetActive(false);//初始加载成功后，要先关闭
                m_UIWindows.Add(windowName, window);
            }

            
            
            return window;
        }
        public void OpenView(UiWindowName windowName)
        {
            var config = uiWindowConfigs.GetWindowConfig(windowName);
            var window = GetWindow(windowName);
            
            //判断该面板是否已经打开
            if(  window.gameObject.activeSelf) return;
            

            //判断互斥
            bool canOpen = true; //是否可以打开
            foreach (var openedWindow in m_UIWindows.Values)
            {
                if(!openedWindow.MBaseView.IsActive) continue;
                
                if ((openedWindow.windowName & config.mutexNames) != 0)
                {
                    canOpen = false;
                    print($"当前要打开的面板:{windowName}讨厌这个已经打开的面板:{openedWindow.windowName}，拒绝干活");
                    break;
                }
            }

            if (canOpen)
            {
                window.Active();
                //模态
                if (config.isModal)
                {
                    window.gameObject.transform.SetParent(GetBelongCanvas(SortingLayer.Modal).transform);
                    m_RtMaskPanel.gameObject.SetActive(true);
                    window.gameObject.transform.SetAsLastSibling();
                }

             
                //关闭其它窗口
                foreach (var openedWindow in m_UIWindows.Values)
                {
                    if(!openedWindow.MBaseView.IsActive) continue;
                    var winName = openedWindow.windowName;
                    if ((config.needCloseNames & winName) != 0)
                    {
                        openedWindow.UnActive(); //关闭
                       
                    }
                }

               
            }
            
        }

     

        public void CloseView(UiWindowName windowName)
        {
            m_RtMaskPanel.gameObject.SetActive(false);
            var closeWin = GetWindow(windowName);
            closeWin.UnActive();
        }
        
        private void Awake()
        {
            _uiManager = this;

            m_UIRoot = GameObject.Find("UIRoot").transform as RectTransform;
            m_RtMaskPanel = m_UIRoot.FindRecursively("UIMaskPanel").transform as RectTransform;
            InitNotificationView();
        }
        # region Private
        private void InitNotificationView()
        {
            ////初始化通知面板
            //UINotification.Instance.notificationView =
            //    Instantiate(prefabUINotification,GetBelongCanvas(SortingLayer.Modal).transform).GetComponent<UINotificationView>();
          
            //UINotification.Instance.notificationView.Close();
        }
        # endregion
    }
}
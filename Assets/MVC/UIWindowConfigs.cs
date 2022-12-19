using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = UnityEngine.Object;

namespace MVC2
{
    public enum SortingLayer
    {
        Default=0,Background=1,Normal,Top,Modal
    }
    
    [Serializable]
    public class UIWindowConfig
    {
        public UiWindowName windowName;

        public GameObject prefab;

        public SortingLayer sortingLayer;
        //是否是模态窗口
        public bool isModal = false;
        [Tooltip("打我开，需要关闭的其它窗口")]
        public UiWindowName needCloseNames;//打我开，需要关闭的其它窗口
        [Tooltip("它些面板开着，我就不开")]
        public UiWindowName mutexNames;//

    }
    
    [CreateAssetMenu(fileName = "UIWindowConfigs", menuName = "MVC2/UIWindowConfigs", order = 0)]
    public class UIWindowConfigs : ScriptableObject
    {
        /// <summary>
        /// 只能在编辑器下使用，当属性面板的值 发生任何变化 都会调用
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void OnValidate()
        {
            foreach (var uiWindowConfig in uiWindowConfigs)
            {
                var needCloseNames = uiWindowConfig.needCloseNames;
                var muteNames = uiWindowConfig.mutexNames;
                var windowName = uiWindowConfig.windowName;

                if ((needCloseNames & windowName) != 0)
                {
                    
                    Debug.LogError($"你不能在{windowName}的配置中，设置关闭自己");
                }
                
                if ((muteNames & windowName) != 0)
                {
                    Debug.LogError($"你不能在{windowName}的配置中，互斥自己");
                }
                
            }
        }

        public List<UIWindowConfig> uiWindowConfigs = new List<UIWindowConfig>();
    
        /// <summary>
        /// 拿到一个容口配置
        /// </summary>
        /// <param name="windowName"></param>
        /// <returns></returns>
        public UIWindowConfig GetWindowConfig(UiWindowName windowName)
        {
            foreach (var uiWindowConfig in uiWindowConfigs)
            {
                if (uiWindowConfig.windowName == windowName)
                {
                    return uiWindowConfig;
                }
            }

            return null;
        }
    }
}
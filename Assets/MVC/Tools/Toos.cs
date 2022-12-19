﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MVC2
{
    public static class Toos
    {
        /// <summary>                        
        /// thi Transform root给Transform添加扩展方法，扩展方法为在根节点查找name的子物体
        /// 查找子物体
        /// </summary>
        /// <param name="root"></需要查找物体的根节点>
        /// <param name="name"></需要查找子物体的名称>
        /// <returns></returns>
        public static Transform FindRecursively(this Transform root, string name)
        {
    
            if (root.name == name)
            {
                return root;
            }
            //遍历根节点下的所有子物体
            foreach (Transform child in root)
            {
                //递归查找子物体
                Transform t = FindRecursively(child, name);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }
    
        public static T FindBehaviour<T>(this Transform root, string name) where T : MonoBehaviour
        {
            Transform child = FindRecursively(root, name);
    
            if (child == null)
            {
                return null;
            }
    
            T temp = child.GetComponent<T>();
            if (temp == null)
            {
                Debug.LogError(name + " is not has component ");
            }
    
            return temp;
        }
    }
}


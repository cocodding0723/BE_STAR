using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityToolBox.Extension
{
    public static class FindInterfaces
    {
        public static List<T> Find<T>(bool includeInactive = false)
        {
            List<T> interfaces = new List<T>();
            GameObject[] rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var rootGameObject in rootGameObjects)
            {
                T[] childrenInterfaces = rootGameObject.GetComponentsInChildren<T>(includeInactive);
                foreach (var childInterface in childrenInterfaces)
                {
                    interfaces.Add(childInterface);
                }
            }
            return interfaces;
        }
    }
}
using UnityEngine;
// Singleton Templete class
// e.g. public class MyClassName : Singleton<MyClassName> {}
// protected MyClassname() {} 을 선언해서 비 싱글톤 생성자 사용을 방지할 것

namespace UnityToolBox.Pattern{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Destroy 여부 확인용
        private static bool s_shuttingDown;
        private static readonly object Lock = new object();
        private static T s_instance;
    
        public static T Instance
        {
            get
            {
                // 게임 종료 시 Object 보다 싱글톤의 OnDestroy 가 먼저 실행 될 수도 있다. 
                // 해당 싱글톤을 gameObject.Ondestory() 에서는 사용하지 않거나 사용한다면 null 체크를 해주자
                if (s_shuttingDown)
                {
                    Debug.Log("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                    return null;
                }
    
                lock (Lock)    //Thread Safe
                {
                    // 인스턴스 존재 여부 확인
                    if (s_instance != null) return s_instance;
                    
                    s_instance = (T)FindObjectOfType(typeof(T));
    
                    // 아직 생성되지 않았다면 인스턴스 생성
                    if (s_instance != null) return s_instance;
                    
                    // 새로운 게임오브젝트를 만들어서 싱글톤 Attach
                    var singletonObject = new GameObject();
                    s_instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
    
                    // Make instance persistent.
                    // DontDestroyOnLoad(singletonObject);
                    return s_instance;
                }
            }
        }
    
        private void OnApplicationQuit()
        {
            s_shuttingDown = true;
        }
    
        private void OnDestroy()
        {
            s_shuttingDown = true;
        }
    }
}
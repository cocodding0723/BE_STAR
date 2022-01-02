using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSceneTest : MonoBehaviour
{
    public string sceneName;
    public void TestButton()
    {
        LoadingControl.Inst.LoadScene(sceneName);
    }
}

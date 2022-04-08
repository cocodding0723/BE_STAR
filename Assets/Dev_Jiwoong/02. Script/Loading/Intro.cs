using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Intro : MonoBehaviour
{
    [SerializeField] private CanvasGroup titleGroup;
    [SerializeField] private GameObject titleObj;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);

        const float duration = 0.5f;
        const float interval = 1.5f;

        AsyncOperation async = SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        async.allowSceneActivation = false;

        Sequence seq = DOTween.Sequence()
                    .Append(titleGroup.DOFade(1.0f,duration))
                    .AppendCallback(() => titleObj.SetActive(true))
                    .AppendInterval(interval)
                    .Append(titleGroup.DOFade(0.0f, duration))
                    .Play();
        
        yield return seq.WaitForCompletion();

        while(async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;
    }
}

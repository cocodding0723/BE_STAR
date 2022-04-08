﻿                                   using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using System;

public class LoadingControl : MonoBehaviour
{
    private static LoadingControl inst;
    public static LoadingControl Inst
    {
        get
        {
            if(inst == null)
            {
                var obj = FindObjectOfType<LoadingControl>();
                if(obj != null)
                {
                    inst = obj;
                }
                else
                {
                    inst = Create();
                }
            }
            return inst;
        }
    }
    private static LoadingControl Create()
    {
        return Instantiate(Resources.Load<LoadingControl>("LoadingUI"));
    }
    private void Awake() 
    {
        if(Inst != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    private CanvasGroup canvasGroup = null;
    [SerializeField]
    private Image progressBar = null;
    private string loadSceneName;
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    [SerializeField] private Image bgImage;

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());
    }
    private IEnumerator LoadSceneProcess()
    {
         bgImage.sprite = sprites[Random.Range(0,sprites.Count)];
         progressBar.fillAmount = 0f;
         yield return StartCoroutine(Fade(true));
         AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
         op.allowSceneActivation = false;
         float timer = 0f;
         while(!op.isDone)
         {
             yield return null;
             if(op.progress <0.9f)
             {
                 progressBar.fillAmount = op.progress;
             }
             else
             {
                 timer += Time.unscaledDeltaTime;
                 progressBar.fillAmount =Mathf.Lerp(0.1f,1f,timer/2);
                 if(progressBar.fillAmount >=1f)
                 {
                     op.allowSceneActivation = true;
                     yield break;
                 }
             }
         }
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while(timer <= 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f,1f,timer) : Mathf.Lerp(1f,0f,timer);
        }

        if(!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}

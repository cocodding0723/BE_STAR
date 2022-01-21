using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public enum MapPageState 
{
    left,
    right,
}




public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject nowSubPage;
    public GameObject preSubPage;
    public AnimationCurve curve;
    public MapPageState mapPageState;
    public Button leftButton;
    public Button rightButton;
    private float speed = 3f;
    private int width;
    private Coroutine subUiAnimation;

    public List<GameObject> mapImages = new List<GameObject>();
    public List<Image> chaperCountImages = new List<Image>();
    public List<Sprite> chaperCountImagesSprite = new List<Sprite>();

    [SerializeField]
    private int mapImageIndex = 0;


    void Start()
    {
        width = Screen.width;
        mapImageIndex = 0;
        if (mapImageIndex <= 0)
        {
            if (leftButton.interactable)
            {
                leftButton.interactable = false;
            }
        }
        else
        {
            if (!leftButton.interactable)
            {
                leftButton.interactable = true;
            }
        }
        ChaperCountImagesSpriteChange();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChaperCountImagesSpriteChange()
    {
        for (int i = 0; i < chaperCountImages.Count; i++)
        {
            if (i == mapImageIndex)
            {
                chaperCountImages[i].sprite = chaperCountImagesSprite[0];
            }
            else
            {
                chaperCountImages[i].sprite = chaperCountImagesSprite[1];
            }
        }
    }


    public void LeftButtonClicked()
    {
        if(mapImages.Count > 0)
        {
            if(mapImageIndex > 0)
            {
                mapImageIndex--;
                ChaperCountImagesSpriteChange();
                NextMapPage(mapImages[mapImageIndex],MapPageState.left);
                if(mapImageIndex <= 0)
                {
                    if(leftButton.interactable)
                    {
                        leftButton.interactable = false;
                    }
                }

                if (!rightButton.interactable)
                {
                    rightButton.interactable = true;
                }

            }
        }
    }
    public void RightButtonClicked()
    {
        if (mapImages.Count > 0)
        {
            if (mapImageIndex >= 0 && mapImageIndex < mapImages.Count)
            {
                mapImageIndex++;
                ChaperCountImagesSpriteChange();
                NextMapPage(mapImages[mapImageIndex], MapPageState.right);
                if (!leftButton.interactable)
                {
                    leftButton.interactable = true;
                }
                if (mapImageIndex >= mapImages.Count - 1)
                {
                    if(rightButton.interactable)
                    {
                        rightButton.interactable = false;
                    }
                }
              
            }
        }
    }



    public void NextMapPage(GameObject subPage, MapPageState _mapPageState)         //텍스트 및 이미지 바꿈
    {
        if(nowSubPage != null)
        {
            preSubPage = nowSubPage;
        }
        nowSubPage = subPage;

        if (nowSubPage != null)
        {
            
            if (subUiAnimation != null)
            {
                PageAnimationStop(preSubPage.GetComponent<RectTransform>());
                StopCoroutine(subUiAnimation);
            }
            
            mapPageState = _mapPageState;

            subUiAnimation = StartCoroutine(AnimationPageOn(nowSubPage.GetComponent<RectTransform>()));

        
        }
    }


    private void PageAnimationStop(RectTransform page)
    {
        page.anchoredPosition = Vector2.zero;
    }



    IEnumerator AnimationPageOn(RectTransform page)
    {
        var a = new Vector2(width, 0);   
        var b = Vector2.zero;
        if (mapPageState == MapPageState.left)
        {
            a = new Vector2(width, 0);
        }
        else
        {
            a = new Vector2(-width, 0);
        }

        page.anchoredPosition = a;
        page.SetAsLastSibling();
        page.gameObject.SetActive(true);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * speed;
            yield return null;
            page.anchoredPosition = Vector2.Lerp(a, b, curve.Evaluate(t));
        }
        page.anchoredPosition = b;

        if(preSubPage != null)
        {
            preSubPage.SetActive(false);
        }
        nowSubPage = page.gameObject;
        


      

    }

}

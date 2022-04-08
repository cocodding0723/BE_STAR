using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    public static ControlManager inst;
    
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;    
    public bool isDraging = false; // ��ġ, �巡�� ����
    private Vector2 startTouch, swipeDelta;
    public static bool isInteracting = true;

    public enum ControlType
    {
        MAIN,
        CHOICE,
        PLAY
    }
    public ControlType controlType = ControlType.MAIN;

    private void Awake() 
    {
        if(!inst)
            inst = this;
    }

    void Update()
    {
        if(controlType == ControlType.CHOICE)
            SwipeControl();
    }
    private void SwipeControl()
    {
        tap = swipeDown = swipeUp = swipeLeft = swipeRight = false; // �⺻ ���´� false

        if (isInteracting)
        {            
                #region PC Version
                if (Input.GetMouseButtonDown(0))
                {  
                    tap = true;
                    isDraging = true;
                    startTouch = Input.mousePosition;
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    isDraging = false;
                    SwipeReset();
                }
                #endregion

                #region Mobile Version
                if (Input.touches.Length > 0)
                {
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        tap = true;
                        isDraging = true;
                        startTouch = Input.touches[0].position;
                    }
                    else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                    {
                        isDraging = false;
                        SwipeReset();
                    }                
                #endregion
            }

            //Calculate the distance
            swipeDelta = Vector2.zero;
            if (isDraging)
            {
                if (Input.touches.Length < 0)
                    swipeDelta = Input.touches[0].position - startTouch;
                else if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                swipeUp = true;
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                swipeDown  = true;
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                swipeLeft = true;
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                swipeRight = true;
            }

            //Did we cross the distance?
            if (swipeDelta.magnitude > 100)
            {
                //Which direction?
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //Left or Right
                    if (x < 0)
                    {
                        swipeLeft = true;
                        Debug.Log("Left");
                    }
                    else
                    {
                        swipeRight = true;
                        Debug.Log("Right");
                    }
                }
                else
                {
                    //Up or Down
                    if (y < 0)
                    {
                        swipeDown = true;
                        Debug.Log("Down");
                    }
                    else
                    {
                        swipeUp = true;
                        Debug.Log("Up");
                    }
                }
                SwipeReset();
            }
        }
        else
        {

        }
    }
    private void SwipeReset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    
}

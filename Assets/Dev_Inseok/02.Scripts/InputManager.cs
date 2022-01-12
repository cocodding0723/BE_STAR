using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    public static InputManager singleton;
    RaycastHit hit;
    float maxDist = 20f;
    public GameObject selectObj;
    Vector3 upPoint;



    // Start is called before the first frame update
    void Start()
    {
        if(singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        CameraRayCast();
    }

    void CameraRayCast()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Debug.DrawRay(Camera.main.transform.position,ray.direction * maxDist, Color.red, 0.3f);
            if (Physics.Raycast(ray, out hit, maxDist, LayerMask.GetMask("Triangle")))
            {
                selectObj = hit.transform.gameObject;
                selectObj.GetComponent<Shapeinteraction>().SelectedObj(true);
            }
        }
        else if(Input.GetMouseButton(0))
        {
            //ui ¶ç¿ì±â
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Debug.DrawRay(Camera.main.transform.position, ray.direction * maxDist, Color.blue, 0.3f);
            if (Physics.Raycast(ray, out hit, maxDist, LayerMask.GetMask("Plane")))
            {
                upPoint = new Vector3(hit.point.x,0, hit.point.z);
                Debug.Log(upPoint);
                if(selectObj != null && upPoint != null)
                {
                    StartCoroutine(selectObj.GetComponent<Shapeinteraction>().MoveSelectObj(upPoint));
                }
            }
        }
    }

    public void LostSelectObj()
    {
        selectObj = null;
    }



  
   






}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    RaycastHit hit;
    float maxDist = 20f;
    public GameObject selectObj;
    Vector3 upPoint;
    // Start is called before the first frame update
    void Start()
    {
        
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
            }
        }
        else if(Input.GetMouseButton(0))
        {
            //ui 띄우기
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
                    StartCoroutine(MoveSelectObj());
                }
            }
        }
    }

    IEnumerator MoveSelectObj()
    {
        float time = 0;
        while(time < 1)
        {
            time += Time.deltaTime;
            var dir = (selectObj.transform.position - upPoint).normalized;
            selectObj.transform.Translate(dir * GetForce() * Time.deltaTime);
            yield return null;
        }
        
    }



    //거리에 따라 힘 가중치
    private float GetForce()
    {
        var dist = Vector3.Distance(selectObj.transform.position, upPoint);
        if(dist > 5f)
        {
            dist = 5;
        }

        return dist;

    }




}

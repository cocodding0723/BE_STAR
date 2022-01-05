using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ShapeItem))]
public class Shapeinteraction : MonoBehaviour
{
    public GameObject trianglePrefab;

    float delay = 2f;
    ShapeItem shapeItem;

    public int crashNumber = 0;


    void Start()
    {
        shapeItem = GetComponent<ShapeItem>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    void CheckExplosion(int myScore)
    {
        if (myScore >= 5)
        {
            if (crashNumber == 4 && myScore == 5)
            {
                //삼각형만 4번 합성되어 별이 됨 폭팔만할것
                StartCoroutine(Explosion());
            }
            else
            {
                //폭팔 후 삼각형 갯수 던지기
                StartCoroutine(Explosion());

                if (myScore >= 5)       //만들기는 매니저로 넘기기
                {
                    shapeItem.shapeScore -= 4;
                    for (int i = 0; i < shapeItem.shapeScore; i++)
                    {
                        CreateTriangle();
                    }
                }
            }
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(delay);

        Destroy(this.gameObject);
        Debug.Log("explosion num : " + shapeItem.shapeScore);


    }

    void CreateTriangle()
    {
        GameObject g = Instantiate(trianglePrefab);
        g.transform.position = transform.position;
        Debug.Log("create Triangle");
        //날려보내기 랜덤값으로
    }




    private void CheckScoreTransformMesh(int myScore)
    {
        if (myScore < 5)
        {
            Debug.Log(myScore);
            switch (myScore)
            {
                case 1:
                    this.tag = "Triangle";
                    //GetComponent<MeshFilter>().mesh = 
                    break;
                case 2:
                    this.tag = "Square";
                    //GetComponent<MeshFilter>().mesh = 
                    break;
                case 3:
                    this.tag = "Pentagon";
                    //GetComponent<MeshFilter>().mesh = 
                    break;
                case 4:
                    this.tag = "Hexagon";
                    //GetComponent<MeshFilter>().mesh = 
                    break;
                case 5:
                    this.tag = "Star";
                    //GetComponent<MeshFilter>().mesh = 
                    break;
                default:
                    Debug.LogError("tag is not shape");
                    break;
            }
        }
    }

    public void StopMove()
    {
        CheckScoreTransformMesh(shapeItem.shapeScore);
    }




    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.GetComponent<ShapeItem>())
        {
            if (GetComponent<ShapeItem>().selectied)
            {
                crashNumber++;
                shapeItem.shapeScore += other.GetComponent<ShapeItem>().shapeScore;
                CheckScoreTransformMesh(shapeItem.shapeScore);
                CheckExplosion(shapeItem.shapeScore);
                Destroy(other.gameObject);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }




}

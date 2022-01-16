using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chanhyeong
{
    [RequireComponent(typeof(ShapeItem))]
    public class Shapeinteraction : MonoBehaviour
    {
        public GameObject trianglePrefab; //생성할 삼각형 프리팹 나중에 오브젝트풀적용하면 알아서 변환

        float delay = 2f; //폭팔까지 텀시간
        ShapeItem shapeItem; //도형정보 클래스

        public int crashNumber = 0; //다른 도형들과 부딪힌 횟수

        void Start()
        {
            shapeItem = GetComponent<ShapeItem>();
        }

        void CheckExplosion(int myScore) // 호출시점 날라가고있는중 다른도형과 부딪혔을때
        {
            if (myScore >= 5) //기본적으로 더해진 값이 별이 되어야 폭팔하기떄문
            {
                if (crashNumber == 4)
                {
                    if (myScore == 5)
                    {
                        //삼각형만 4번 합성되어 별이 됨 폭팔만할것
                        NormalCase();
                    }
                    else if (myScore > 5)
                    {
                        PlusScoreCase(); // 추가점수 + 삼각형 추가생성
                    }
                }
                else
                {
                    //폭팔 후 삼각형 갯수 던지기
                    if (myScore >= 5)
                    {
                        PlusScoreCase(); // 추가점수 + 삼각형 추가생성
                    }
                }
            }

        }

        void PlusScoreCase()
        {
            StartCoroutine(Explosion());

            shapeItem.shapeScore -= 4;
            for (int i = 0; i < shapeItem.shapeScore; i++)
            {
                CreateTriangle();
            }
        }

        void NormalCase()
        {
            StartCoroutine(Explosion());
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

        private void CheckScoreTransformMesh(int myScore) // 도형변환 함수
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

        public void StopMove() //물리에서 멈추면 호출
        {
            CheckScoreTransformMesh(shapeItem.shapeScore);
            SelectedObj(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if (other.GetComponent<ShapeItem>()) //부딪힌게 도형인지아닌지
            {
                if (GetComponent<ShapeItem>().selectied) // 선택해서 던진도형인가
                {
                    crashNumber++; //부딪힌 횟수 +
                    shapeItem.shapeScore += other.GetComponent<ShapeItem>().shapeScore; //스코어 부딪힌 도형점수만큼 더해줌
                    CheckScoreTransformMesh(shapeItem.shapeScore); // 도형변환 함수
                    CheckExplosion(shapeItem.shapeScore); // 폭팔체크
                    Destroy(other.gameObject); // 부딪힌 오브젝트 파괴 오브젝트풀 할시 변환
                }
            }
        }

        public void SelectedObj(bool t)
        {
            if (GetComponent<BoxCollider>())
            {
                GetComponent<BoxCollider>().isTrigger = t;
            }

            shapeItem.selectied = t;
        }

        public IEnumerator MoveSelectObj(Vector3 upPoint)
        {
            float time = 0;
            while (time < 1)
            {
                time += Time.deltaTime;
                var dir = (transform.position - upPoint).normalized;
                transform.Translate(dir * GetForce(upPoint) * Time.deltaTime);
                yield return null;
            }

            StopMove();
        }

        //거리에 따라 힘 가중치
        private float GetForce(Vector3 upPoint)
        {
            var dist = Vector3.Distance(transform.position, upPoint);
            if (dist > 5f)
            {
                dist = 5;
            }

            return dist;

        }

    }
}
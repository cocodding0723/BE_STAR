using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (ShapeItem))]
public class Shapeinteraction : MonoBehaviour
{
    public GameObject trianglePrefab;   //������ �ﰢ�� ������ ���߿� ������ƮǮ�����ϸ� �˾Ƽ� ��ȯ

    float delay = 2f;   //���ȱ��� �ҽð�
    ShapeItem shapeItem;    //�������� Ŭ����

    public int crashNumber = 0; //�ٸ� ������� �ε��� Ƚ��


    void Start()
    {
        shapeItem = GetComponent<ShapeItem>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    void CheckExplosion(int myScore) // ȣ����� ���󰡰��ִ��� �ٸ������� �ε�������
    {
        if (myScore >= 5)       //�⺻������ ������ ���� ���� �Ǿ�� �����ϱ⋚��
        {
            if (crashNumber == 4)
            {
                if (myScore == 5)
                {
                    //�ﰢ���� 4�� �ռ��Ǿ� ���� �� ���ȸ��Ұ�
                    NormalCase();
                }
                else if(myScore > 5)
                {
                    PlusScoreCase();// �߰����� + �ﰢ�� �߰�����
                }
            }
            else
            {
                //���� �� �ﰢ�� ���� ������
                if (myScore >= 5)      
                {
                    PlusScoreCase(); // �߰����� + �ﰢ�� �߰�����
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
        //���������� ����������
    }




    private void CheckScoreTransformMesh(int myScore)  // ������ȯ �Լ�
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

    public void StopMove()  //�������� ���߸� ȣ��
    {
        CheckScoreTransformMesh(shapeItem.shapeScore);
        SelectedObj(false);
    }




    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.GetComponent<ShapeItem>()) //�ε����� ���������ƴ���
        {
            if (GetComponent<ShapeItem>().selectied) // �����ؼ� ���������ΰ�
            {
                crashNumber++;  //�ε��� Ƚ�� +
                shapeItem.shapeScore += other.GetComponent<ShapeItem>().shapeScore; //���ھ� �ε��� ����������ŭ ������
                CheckScoreTransformMesh(shapeItem.shapeScore);  // ������ȯ �Լ�
                CheckExplosion(shapeItem.shapeScore); // ����üũ
                Destroy(other.gameObject); // �ε��� ������Ʈ �ı� ������ƮǮ �ҽ� ��ȯ
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
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
        InputManager.singleton.LostSelectObj();



    }

    //�Ÿ��� ���� �� ����ġ
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

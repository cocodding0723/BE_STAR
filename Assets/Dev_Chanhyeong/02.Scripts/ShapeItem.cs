using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chanhyeong
{
    public class ShapeItem : MonoBehaviour
    {
        public int shapeScore; //1 �ﰢ 2 �簢 3 ���� 4 ���� 5 �� 
        public string shapeTag;
        public bool selectied = false; // ��������

        void Start()
        {
            shapeTag = this.tag;
        }
    }
}
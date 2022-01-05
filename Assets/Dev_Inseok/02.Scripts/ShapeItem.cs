using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeItem : MonoBehaviour
{

    public int shapeScore;
    public string shapeTag;
    public bool selectied = false;



    void Start()
    {
        shapeTag = this.tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

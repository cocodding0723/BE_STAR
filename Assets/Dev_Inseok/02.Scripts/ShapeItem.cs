using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeItem : MonoBehaviour
{

    public int shapeScore;      //1 »ï°¢ 2 »ç°¢ 3 ¿À°¢ 4 À°°¢ 5 º° 
    public string shapeTag;
    public bool selectied = false;  // ¼¿·º»óÅÂ



    void Start()
    {
        shapeTag = this.tag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

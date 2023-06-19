using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    public GameObject c1, c2, c3, c4;
    // Start is called before the first frame update
    void Start()
    {
        GameManager gm = FindObjectOfType<GameManager>();

        Color cube1 = c1.GetComponent<MeshRenderer>().material.color;
        Color cube2 = c2.GetComponent<MeshRenderer>().material.color;
        Color cube3 = c3.GetComponent<MeshRenderer>().material.color;
        Color cube4 = c4.GetComponent<MeshRenderer>().material.color;

        // controls alpha of cube colors 
        if (gm != null) {
            if (gm.winter) {
                cube1.a = 1;
                cube2.a = 1;
                cube3.a = 1;
                cube4.a = 1;
            } else if (gm.fall) {
                cube1.a = 0;
                cube2.a = 0;
                cube3.a = 1;
                cube4.a = 0;
            } else if (gm.summer) {
                cube1.a = 0;
                cube2.a = 1;
                cube3.a = 0;
                cube4.a = 0;
            } else if (gm.spring) {
                cube1.a = 0;
                cube2.a = 1;
                cube3.a = 0;
                cube4.a = 0;
            } else {
                cube1.a = 1;
                cube2.a = 0;
                cube3.a = 0;
                cube4.a = 0;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubeSelect : MonoBehaviour
{
    private Color cubeColor;
    [SerializeField] private string cubeSceneName;

    // Start is called before the first frame update
    void Start()
    {
        cubeColor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver() {
        if (cubeColor.a == 1 && Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene(cubeSceneName);
        }
    }
}

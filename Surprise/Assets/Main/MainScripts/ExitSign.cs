using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSign : MonoBehaviour
{
    public Color lockedColor;
    public Color unlockedColor;

    [SerializeField] private Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameManager>().Completed()) {
            mat.SetColor("_EmissionColor", unlockedColor);
        }
        else {
           mat.SetColor("_EmissionColor", lockedColor); 
        }
    }
}

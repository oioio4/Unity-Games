using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillateLight : MonoBehaviour
{
    private Light curLight;

    public float maxIntensity;

    // Start is called before the first frame update
    private void Start()
    {
        curLight = GetComponent<Light>();
    }

    // Update is called once per frame
    private void Update()
    {
        curLight.intensity = 1 + Mathf.PingPong(Time.time * 0.5f, maxIntensity);
    }
}

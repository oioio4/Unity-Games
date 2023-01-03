using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeBar : MonoBehaviour
{
    public Image CubeBarFill;
    public float targetPower = 0f;
    private float currentPower = 0f;
    [SerializeField] private float maxPower = 10f;

    private void Awake() {
        currentPower = 0f;
    }

    private void Update() {
        currentPower = Mathf.Lerp(currentPower, targetPower, Time.deltaTime);

        CubeBarFill.fillAmount = (currentPower / maxPower);
    }
}

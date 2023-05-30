using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration = 1f;

    public void Shake() {
        StartCoroutine(Shaking());
    }

    IEnumerator Shaking() {
        Vector3 startPosition = transform.position;
        float t = 0f;

        while (t < duration) {
            t += Time.deltaTime;
            float strength = curve.Evaluate(t / duration);
            transform.position = startPosition + Random.insideUnitSphere;
            yield return null;
        }

        transform.position = startPosition;
    }
}

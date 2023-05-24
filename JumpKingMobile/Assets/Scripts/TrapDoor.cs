using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public float delay = 5f;

    void Start()
    {
        StartCoroutine(loop(delay));
    }

    bool rotating = false;

    IEnumerator rotateObject(Quaternion newRot, float duration)
    {
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        Quaternion currentRot = gameObject.transform.rotation;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObject.transform.rotation = Quaternion.Lerp(currentRot, newRot, counter / duration);
            yield return null;
        }
        rotating = false;
    }

    IEnumerator loop(float delayTime) {
        Quaternion rot1 = Quaternion.Euler(new Vector3(0, 0, -90));
        Quaternion rot2 = Quaternion.Euler(new Vector3(0, 0, 0));

        while (true) {
            StartCoroutine(rotateObject(rot1, 0.2f));
            yield return new WaitForSeconds(delayTime);
            StartCoroutine(rotateObject(rot2, 0.2f));
            yield return new WaitForSeconds(delayTime);
        }
    }
}

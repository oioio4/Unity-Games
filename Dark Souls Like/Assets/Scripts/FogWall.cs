using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class FogWall : MonoBehaviour
    {
        private void Awake() {
            gameObject.SetActive(false);
        }

        public void ActivateFogWall() {
            gameObject.SetActive(true);
        }

        public void DeactivateFogWall() {
            gameObject.SetActive(false);
        }
    }
}

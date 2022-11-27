using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NC
{
    public class FocusPointBar : MonoBehaviour
    {
        private Slider slider;

        private void Awake() {
            slider = GetComponent<Slider>();
        }

        public void SetMaxFocusPoints(float maxFocusPoints) {
            slider.maxValue = maxFocusPoints;
            slider.value = maxFocusPoints; 
        }

        public void SetCurrentFocusPoints(float currentFocusPoints) {
            slider.value = currentFocusPoints;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NC
{
    public class StaminaBar : MonoBehaviour
    {
        private Slider slider;

        private void Awake() {
            slider = GetComponent<Slider>();
        }

        public void SetMaxStamina(float maxStamina) {
            slider.maxValue = maxStamina;
            slider.value = maxStamina; 
        }

        public void SetCurrentStamina(float currentStamina) {
            slider.value = currentStamina;
        }
    }
}

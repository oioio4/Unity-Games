using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NC
{
    public class UIEnemyHealthBar : MonoBehaviour
    {
        Slider slider;
        Camera mainCamera;
        float timeUntilBarHidden = 0;

        private void Awake() {
            slider = GetComponentInChildren<Slider>();
            mainCamera = FindObjectOfType<Camera>();
        }

        public void SetHealth(int health) {
            slider.value = health;
            timeUntilBarHidden = 3;
        }

        public void SetMaxHealth(int maxHealth) {
            slider.maxValue = maxHealth;
            slider.value = maxHealth;
        }

        private void Update() {
            timeUntilBarHidden -= Time.deltaTime;

            if (slider != null) {
                if (timeUntilBarHidden <= 0) {
                    timeUntilBarHidden = 0;
                    slider.gameObject.SetActive(false);
                }
                else {
                    if (!slider.gameObject.activeInHierarchy) {
                        slider.gameObject.SetActive(true);
                    }
                }

                if (slider.value <= 0) {
                    Destroy(slider.gameObject);
                }
            }
        }

        private void LateUpdate()
        {
            if(slider != null)
            {
                transform.forward = mainCamera.transform.forward;
            }
        }
    }
}

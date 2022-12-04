using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class FootModelChanger : MonoBehaviour
    {
        public List<GameObject> footModels;

        private void Awake() {
            GetAllFootModels();
        }

        private void GetAllFootModels() {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++) {
                footModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllFootModels() {
            foreach (GameObject footModel in footModels) {
                footModel.SetActive(false);
            }
        }

        public void EquipFootModel(string footName) {
            for (int i = 0; i < footModels.Count; i++) {
                if (footModels[i].name == footName) {
                    footModels[i].SetActive(true);
                    break;
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class LegModelChanger : MonoBehaviour
    {
        public List<GameObject> legModels;

        private void Awake() {
            GetAllLegModels();
        }

        private void GetAllLegModels() {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++) {
                legModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllLegModels() {
            foreach (GameObject legModel in legModels) {
                legModel.SetActive(false);
            }
        }

        public void EquipLegModel(string legName) {
            for (int i = 0; i < legModels.Count; i++) {
                if (legModels[i].name == legName) {
                    legModels[i].SetActive(true);
                    break;
                }
            }
        }
    }
}

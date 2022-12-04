using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class CapeModelChanger : MonoBehaviour
    {
        public List<GameObject> capeModels;

        private void Awake() {
            GetAllCapeModels();
        }

        private void GetAllCapeModels() {
            int childrenGameObjects = transform.childCount;

            for (int i = 0; i < childrenGameObjects; i++) {
                capeModels.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnequipAllCapeModels() {
            foreach (GameObject capeModel in capeModels) {
                capeModel.SetActive(false);
            }
        }

        public void EquipCapeModel(string capeName) {
            for (int i = 0; i < capeModels.Count; i++) {
                if (capeModels[i].name == capeName) {
                    capeModels[i].SetActive(true);
                    break;
                }
            }
        }
    }
}

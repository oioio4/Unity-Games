using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class IllusionaryWall : MonoBehaviour
    {
        public bool wallHit;
        public Material IllusionaryWallMaterial;
        public float alpha;
        public float fadeTimer = 2.5f;
        public Collider wallCollider;

        private Renderer rend;

        private void Start() {
            rend = GetComponent<Renderer>();
            rend.material = new Material(IllusionaryWallMaterial);
        }

        private void Update() {
            if (wallHit) {
                FadeIllusionaryWall();
            } 
        }

        public void FadeIllusionaryWall() {
            alpha = rend.material.color.a;
            alpha = alpha - Time.deltaTime / fadeTimer;
            Color fadedWallColor = new Color(1, 1, 1, alpha);
            rend.material.color = fadedWallColor;

            if (wallCollider.enabled) {
                wallCollider.enabled = false;
            }

            if (alpha <= 0) {
                Destroy(this);
            }
        }
    }
}

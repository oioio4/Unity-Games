using UnityEngine;

// Shows the season cubes on the level select: unlocked ones solid, locked ones as a faint ghost.
public class CubeManager : MonoBehaviour
{
    public GameObject c1, c2, c3, c4;

    [SerializeField] private float lockedAlpha = 0.08f;

    void Start()
    {
        foreach (GameObject cube in new[] { c1, c2, c3, c4 }) {
            if (cube == null || !cube.TryGetComponent(out CubeSelect select)) continue;

            // the cube materials are transparent (fade), so alpha does the hiding
            var material = cube.GetComponent<MeshRenderer>().material;
            Color color = material.color;
            color.a = select.Unlocked ? 1f : lockedAlpha;
            material.color = color;
        }
    }
}

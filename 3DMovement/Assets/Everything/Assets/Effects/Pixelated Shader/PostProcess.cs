using UnityEngine;

[ExecuteInEditMode]
public class PostProcess : MonoBehaviour
{
    [SerializeField]
    private Vector2 cellSize = new Vector2(4, 4);

    private Material material;

    private void Awake()
    {
        material = new Material(Shader.Find("Hidden/Pixelated"));
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetFloat("_ScreenWidth", Screen.width);
        material.SetFloat("_ScreenHeight", Screen.height);
        material.SetFloat("_CellSizeX", cellSize.x);
        material.SetFloat("_CellSizeY", cellSize.y);
        Graphics.Blit(source, destination, material);
    }
}





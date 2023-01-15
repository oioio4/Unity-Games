using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public Shader shader;
    public Texture texture;

    private Material _material;

    void Start()
    {
        _material = new Material(shader);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        _material.SetTexture("u_maskTex", texture);
        _material.SetVector("u_resolution", new Vector2(Screen.width, Screen.height));
        Graphics.Blit(source, destination, _material);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect : MonoBehaviour
{
    Camera AttachedCamera;

    public Shader PostOutline;
    public Texture PaletteMat;
    public Texture NoiseMat;

    public Material PostMat;
    public Color VignetteColor;

    void Start()
    {
        AttachedCamera = GetComponent<Camera>();
        AttachedCamera.depthTextureMode = DepthTextureMode.Depth;

        PostMat = new Material(PostOutline);
        PostMat.SetTexture("_PaletteTex", PaletteMat);
        PostMat.SetTexture("_NoiseTex", NoiseMat);
        PostMat.SetColor("_VignetteColor", VignetteColor);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, PostMat);
    }
}

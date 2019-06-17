using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffect : MonoBehaviour
{
    // Start is called before the first frame update
    Camera AttachedCamera;
    public Shader PostOutline;
    public Texture PaletteMat;
    Material PostMat;

    void Start()
    {
        AttachedCamera = GetComponent<Camera>();
        AttachedCamera.depthTextureMode = DepthTextureMode.Depth;

        PostMat = new Material(PostOutline);
        PostMat.SetTexture("_PaletteTex", PaletteMat);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, PostMat);
    }
}

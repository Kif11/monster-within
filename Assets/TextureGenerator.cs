using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator : MonoBehaviour
{
    Texture2D texture;
    public int resolution = 256;

    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D(resolution, resolution, TextureFormat.RGB24, true);
        texture.name = "Procedural Texture";
        GetComponent<MeshRenderer>().material.mainTexture = texture;
        FillTexture();
    }



    private void FillTexture()
    {
        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                float col = Mathf.PerlinNoise(15f* Mathf.Abs(x-0.5f*resolution) / resolution,15f* Mathf.Abs(y - 0.5f*resolution) / resolution);
                Color c = new Color(col, col, col);
                texture.SetPixel(x, y, c);
            }
        }
        texture.Apply();
        SaveTextureAsPNG();
    }

    public void SaveTextureAsPNG()
    {
        string fullPath = Application.dataPath + "/Textures/perlinTex.png";
        byte[] _bytes = texture.EncodeToPNG();
        System.IO.File.WriteAllBytes(fullPath, _bytes);
        Debug.Log(_bytes.Length / 1024 + "Kb was saved as: " + fullPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource cafeMusic;
    void Start()
    {
        cafeMusic = gameObject.GetComponents<AudioSource>()[0];
        cafeMusic.volume = 0.0f;
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        while(cafeMusic.volume < 0.5)
        {
            cafeMusic.volume += (Time.deltaTime * 0.01f);
            yield return null;
        }
        yield return 0;
    }

    IEnumerator FadeInPitchDistortion()
    {
        while(cafeMusic.pitch > -0.3)
        {
            cafeMusic.pitch -= (Time.deltaTime);
            yield return null;
        }
        yield return 0;
    }

    IEnumerator FadeOutPitchDistortion()
    {
        while(cafeMusic.pitch < 1)
        {
            cafeMusic.pitch += (Time.deltaTime);
            yield return null;
        }
        yield return 0;
    }

    public void startFadeInPitch() {
      StopCoroutine("FadeOutPitchDistortion");
      StartCoroutine("FadeInPitchDistortion");
    }

    public void startFadeOutPitch() {
      StopCoroutine("FadeInPitchDistortion");
      StartCoroutine("FadeOutPitchDistortion");
    }

}

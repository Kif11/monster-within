using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipUtils : MonoBehaviour
{
    public GameObject tentacle;
    public GameObject hand;
    public GameObject ambientSounds;
    public GameObject hearts;
    private Renderer renderer;

    private bool blink;
    private float blinkVal;
    private float blushAmount;
    private float heartAmount;

    PostEffect postEffect;
    AudioSource monsterSound;

    // Start is called before the first frame update
    void Start()
    {
        postEffect = Camera.main.GetComponent<PostEffect>();
        monsterSound = gameObject.GetComponent<AudioSource>();

        GameObject body = gameObject.transform.Find("body_mesh").gameObject;
        renderer = body.GetComponent<Renderer>();
        blushAmount = 0.0f;

        blinkVal = 0;
        postEffect.PostMat.SetFloat("_BlinkAmount", 0f);
    }

    // Update is called once per frame
    void Update()
    {

        //TODO ; Make Coroutine for this
        if (blink)
        {
            blinkVal += 4*Time.deltaTime;
            if (blinkVal > Mathf.PI)
            {
                blink = false;
                postEffect.PostMat.SetFloat("_BlinkAmount", 0f);
            }
            else
            {
                postEffect.PostMat.SetFloat("_BlinkAmount", 2f*Mathf.Sin(blinkVal));
            }
        }
    }

    public void SetMonsterMode()
    {
        hand.SetActive(false);
        tentacle.SetActive(true);
        postEffect.PostMat.SetFloat("_VignetteAmount", 8.0f);
        monsterSound.Play();
        AudioController controller = ambientSounds.GetComponent<AudioController>();
        controller.startFadeInPitch();

    }
    public void SetHumanMode()
    {
        hand.SetActive(true);
        tentacle.SetActive(false);
        postEffect.PostMat.SetFloat("_VignetteAmount", 0.0f);
        monsterSound.Pause();
        AudioController controller = ambientSounds.GetComponent<AudioController>();
        controller.startFadeOutPitch();
    }
    public void Blink()
    {
        blink = true;
        blinkVal = 0;
    }

    public void StartFadeInHearts()
    {
        StopCoroutine("FadeOutHearts");
        StartCoroutine("FadeInHearts");
    }

    public void StartFadeOutHearts()
    {
        heartAmount = 0.0f;
    }

    IEnumerator FadeInHearts()
    {
        while (heartAmount < 7.3f)
        {
            heartAmount += 3.6f * Time.deltaTime;
            for (int i = 0; i < 2; i ++) 
            {
                Transform heart = hearts.transform.GetChild(i);
                float x = Mathf.Min(heartAmount - 0.3f * i + 1.3f, 7f);
                float fx = Mathf.Max(0.75f * (Mathf.Sin(-x) + Mathf.Sin(-2f*x) + 1f), 0f);
                heart.localScale = fx * new Vector3(1f, 1f, 1f);
            }
            yield return null;
        }
        yield return 0;
    }

    public void StartFadeInBlush()
    {
        StopCoroutine("FadeOutBlush");
        StartCoroutine("FadeInBlush");
    }

    public void StartFadeOutBlush()
    {
        StartCoroutine("FadeOutBlush");
        StopCoroutine("FadeInBlush");
    }

    IEnumerator FadeInBlush()
    {
        while (blushAmount < 1)
        {
            blushAmount += Time.deltaTime;
            renderer.material.SetFloat("_BlushAmount", blushAmount);
            yield return null;
        }
        yield return 0;
    }

    IEnumerator FadeOutBlush()
    {
        while (blushAmount > 0)
        {
            blushAmount -= Time.deltaTime;
            renderer.material.SetFloat("_BlushAmount", blushAmount);
            yield return null;
        }
        yield return 0;
    }
}

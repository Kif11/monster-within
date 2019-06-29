using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipUtils : MonoBehaviour
{
    public GameObject tentacle;
    public GameObject hand;
    public GameObject ambientSounds;
    private Renderer renderer;

    private bool blink;
    private float blinkVal;
    private float blushAmount;

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

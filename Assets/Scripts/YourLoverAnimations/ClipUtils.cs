using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipUtils : MonoBehaviour
{
    public GameObject tentacle;
    public GameObject staticTentacles;
    public GameObject hand;
    public GameObject ambientSounds;
    public GameObject hearts;
    public GameObject tears;
    public GameObject raycaster;
    public GameObject IKTarget;

    private Renderer charRenderer;

    private bool blink;
    private float blinkVal;
    private float blushAmount;
    private float heartAmount;
    private float tearAmount;
    private float lerpAmount;
    private float hopAmount;
    private Quaternion fromQuaternion;

    PostEffect postEffect;
    AudioSource monsterSound;

    // Start is called before the first frame update
    void Start()
    {
        postEffect = Camera.main.GetComponent<PostEffect>();
        monsterSound = gameObject.GetComponent<AudioSource>();

        GameObject body = gameObject.transform.Find("body_mesh").gameObject;
        charRenderer = body.GetComponent<Renderer>();

        blushAmount = 0.0f;

        tearAmount = 0.0f;
        lerpAmount = 0.0f;
        hopAmount = Mathf.PI / 2.0f;

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

    public void SetMonsterMode(bool halfMode)
    {
        hand.SetActive(false);
        raycaster.SetActive(false);

        IKTarget.SetActive(true);
        tentacle.SetActive(true);
        if (!halfMode)
        {
            staticTentacles.SetActive(true);
        }
        postEffect.PostMat.SetFloat("_VignetteAmount", 8.0f);
        monsterSound.Play();
        AudioController controller = ambientSounds.GetComponent<AudioController>();
        controller.startFadeInPitch();
    }

    public void SetHumanMode()
    {
        hand.SetActive(true);
        raycaster.SetActive(true);

        IKTarget.SetActive(false);
        tentacle.SetActive(false);
        staticTentacles.SetActive(false);
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

    public void StartFadeInSweat()
    {
        StartCoroutine("FadeInSweat");
        StartCoroutine("BounceSweat");
    }

    public void StartFadeOutSweat()
    {
        StopCoroutine("FadeInSweat");
        StartCoroutine("FadeOutSweat");
    }

    IEnumerator FadeOutSweat()
    {
        while (tearAmount > 0f)
        {
            tearAmount -= Time.deltaTime;
            tears.transform.localScale = tearAmount * new Vector3(1f, 1f, 1f);
            yield return null;
        }
        StopCoroutine("BounceSweat");
        yield return 0;
    }

    IEnumerator FadeInSweat()
    {
        while (tearAmount < 1f)
        {
            tearAmount += 0.25f * Time.deltaTime;
            tears.transform.localScale = tearAmount * new Vector3(1f, 1f, 1f);
            yield return null;
        }
        yield return 0;
    }

    IEnumerator BounceSweat()
    {
        while (true)
        {
            Vector3 localPos = tears.transform.localPosition;
            localPos.y = 2.9f + 0.5f * (Mathf.Sin(1.5f * Time.fixedTime) + Mathf.Cos(3f * Time.fixedTime));
            tears.transform.localPosition = localPos;
            yield return null;
        }
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
            charRenderer.material.SetFloat("_BlushAmount", blushAmount);
            yield return null;
        }
        yield return 0;
    }

    IEnumerator FadeOutBlush()
    {
        while (blushAmount > 0)
        {
            blushAmount -= Time.deltaTime;
            charRenderer.material.SetFloat("_BlushAmount", blushAmount);
            yield return null;
        }
        yield return 0;
    }

    public void StartRunAway()
    {
        Quaternion targetQuat = Quaternion.Euler(0, 0, 0);
        Vector3 targetPos = new Vector3(0f, 0f, 20f);
        StartCoroutine("RotateToTarget", targetQuat);
        StartCoroutine("MoveToTarget", targetPos);
    }

    IEnumerator RotateToTarget(Quaternion target)
    {
        fromQuaternion = transform.rotation;
        while (lerpAmount < 1.0f)
        {
            lerpAmount += Time.deltaTime; 
            transform.rotation = Quaternion.Lerp(fromQuaternion, target, lerpAmount);
            yield return null;
        }
        yield return 0;
    }
    IEnumerator MoveToTarget(Vector3 target)
    {
        while (transform.position != target)
        {
            hopAmount += Time.deltaTime;
            Vector3 pos = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
            pos.y = 0.626f + 0.1f * Mathf.Abs(Mathf.Sin(2f * hopAmount) + Mathf.Cos(4f * hopAmount));
            transform.position = pos;
            yield return null;
        }
        yield return 0;
    }

    public void StartMovePlayerTowards(GameObject player, Vector3 target)
    {
        object[] cparams = new object[] { player, target };
        StartCoroutine("MovePlayerTowards", cparams);
    }

    IEnumerator MovePlayerTowards(object[] cparams)
    {
        GameObject player = (GameObject)cparams[0];
        Vector3 target = (Vector3)cparams[1];

        while (player.transform.position != target)
        {
            Vector3 pos = Vector3.MoveTowards(player.transform.position, target, Time.deltaTime);
            player.transform.position = pos;
            yield return null;
        }
        yield return 0;
    }
}

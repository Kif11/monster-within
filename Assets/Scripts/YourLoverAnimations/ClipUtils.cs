using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipUtils : MonoBehaviour
{
    public GameObject tentacle;
    public GameObject hand;
    private bool blink;
    private float blinkVal;

    PostEffect postEffect;

    // Start is called before the first frame update
    void Start()
    {
        postEffect = Camera.main.GetComponent<PostEffect>();
        blinkVal = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (blink)
        {
            blinkVal += 4*Time.deltaTime; 
            if (blinkVal > Mathf.PI)
            {
                blink = false;
            }
            postEffect.PostMat.SetFloat("_BlinkAmount", 2f*Mathf.Sin(blinkVal));
        }
    }

    public void SetMonsterMode()
    {
        hand.SetActive(false);
        tentacle.SetActive(true);
        postEffect.PostMat.SetFloat("_VignetteAmount", 8.0f);
    }
    public void SetHumanMode()
    {
        hand.SetActive(true);
        tentacle.SetActive(false);
        postEffect.PostMat.SetFloat("_VignetteAmount", 0.0f);
    }
    public void Blink()
    {
        blink = true;
        blinkVal = 0;
    }
}

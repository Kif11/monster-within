using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitItem : MenuItemBasic
{
    [SerializeField] public string action;
    [SerializeField] public bool handleHands = true;
    [SerializeField] GameObject hand1;
    [SerializeField] GameObject hand2;

    public override void OnClick()
    {
        if(action == "yes")
        {
            // quit app
            Application.Quit();
        }
        else if (action == "no")
        {
            transform.parent.gameObject.SetActive(false);

            if (handleHands)
            {
                if (Char.GetComponent<ClipUtils>().isInMonsterMode)
                {
                    Char.GetComponent<ClipUtils>().hand1.SetActive(false);
                    Char.GetComponent<ClipUtils>().hand2.SetActive(false);
                }
            }
            else
            {
                hand1.SetActive(false);
                hand2.SetActive(false);
            }

            Time.timeScale = 1.0f;
        }
        outlineMat.SetFloat("_OutlineIntensity", 1f);
        //play click sound
        audioSources[0].Play();
    }

    public override void Update()
    {
        if (handleHands)
        {
            Char.GetComponent<ClipUtils>().hand1.SetActive(true);
            Char.GetComponent<ClipUtils>().hand2.SetActive(true);
        }
        else
        {
            hand1.SetActive(true);
            hand2.SetActive(true);
        }
        Time.timeScale = 0.0f;
    }
}

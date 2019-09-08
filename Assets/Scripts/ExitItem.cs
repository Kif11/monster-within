using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitItem : MenuItemBasic
{
    [SerializeField] public string action;

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
            if (Char.GetComponent<ClipUtils>().isInMonsterMode)
            {
                Char.GetComponent<ClipUtils>().hand1.SetActive(false);
                Char.GetComponent<ClipUtils>().hand2.SetActive(false);
            }
            Time.timeScale = 1.0f;
        }
        outlineMat.SetFloat("_OutlineIntensity", 1f);
        //play click sound
        audioSources[0].Play();
    }

    public override void Update()
    {
        Char.GetComponent<ClipUtils>().hand1.SetActive(true);
        Char.GetComponent<ClipUtils>().hand2.SetActive(true);
        Time.timeScale = 0.0f;
    }
}

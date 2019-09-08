using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingTimeManager : MonoBehaviour
{
    public float sceneDuration;
    public string nextSceneName;
    private float startTime;
    private float blinkVal;

    private PostEffect postEffect;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.fixedTime;
        postEffect = Camera.main.GetComponent<PostEffect>();
        blinkVal = 0;
  }

    // Update is called once per frame
    void Update()
    {
        if(Time.fixedTime - startTime > sceneDuration){
          //start camera fade out
          blinkVal += 4 * Time.deltaTime;
          postEffect.PostMat.SetFloat("_BlinkAmount", blinkVal);
      }

      if (blinkVal > Mathf.PI)
      {
        //change scene
        SceneManager.LoadScene(nextSceneName);
    }

    }
}

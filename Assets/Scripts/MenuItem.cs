using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    protected Animator animator;
    protected Material outlineMat;
    protected float secondsFromClick;
    protected AudioSource[] audioSources;

    public GameObject Char;
    public GameObject prefab;
    public int animatorParam; 

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        animator = Char.GetComponent<Animator>();
        Renderer r = GetComponent<Renderer>();
        outlineMat = r.material;
        outlineMat.SetFloat("_OutlineIntensity", 0.0f);

        secondsFromClick = 0.0f;
    }

    // Update is called once per frame
    void RemoveOutline()
    {
        outlineMat.SetFloat("_OutlineIntensity", 0.0f);

    }

    public virtual void Update()
    {
        secondsFromClick += Time.deltaTime;
    }

    public void OnHover()
    {
        outlineMat.SetFloat("_OutlineIntensity", 1f);
    }

    public void OnUp()
    {
        outlineMat.SetFloat("_OutlineIntensity", 0.0f);
    }

    public virtual void OnClick()
    {
        if(secondsFromClick < 0.5)
        {
            return;
        }
        // start a certain animation 
        GameObject anotherSushi = Instantiate(prefab, new Vector3(-0.507f, 1.34f, -2.73f), Quaternion.identity);
        SushiGlobalData.sushiQueue.Enqueue(anotherSushi);

        // only if we are in idle state 
        outlineMat.SetFloat("_OutlineIntensity", 1f);
        animator.SetInteger("menuItemID", animatorParam);
        secondsFromClick = 0.0f;

        //play click sound
        audioSources[0].Play();
    }
}

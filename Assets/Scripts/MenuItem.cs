using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    private Animator animator;
    private Material outlineMat;
    private float secondsFromClick;
    private AudioSource audioSource;

    public GameObject Char;
    public GameObject prefab;
    public int animatorParam; 

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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

    private void Update()
    {
        secondsFromClick += Time.deltaTime;
    }

    public void OnClick()
    {
        if(secondsFromClick < 0.5)
        {
            return;
        }
        // start a certain animation 
        GameObject anotherSushi = Instantiate(prefab, new Vector3(-0.507f, 1.34f, -2.73f), Quaternion.identity);
        SushiGlobalData.sushiQueue.Enqueue(anotherSushi);

        // only if we are in idle state 
        outlineMat.SetFloat("_OutlineIntensity", 2.5f);
        Invoke("RemoveOutline", 0.5f);
        animator.SetInteger("menuItemID", animatorParam);
        secondsFromClick = 0.0f;

        //play click sound
        audioSource.Play();


    }
}

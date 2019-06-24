using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuItem : MonoBehaviour
{
    private Animator animator;
    private Material outlineMat;

    public GameObject Char;
    public int animatorParam; 

    // Start is called before the first frame update
    void Start()
    {
        animator = Char.GetComponent<Animator>();
        Renderer r = GetComponent<Renderer>();
        outlineMat = r.material;
        outlineMat.SetFloat("_OutlineIntensity", 0.0f);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        // start a certain animation 
        // only if we are in idle state 
        if( animator.GetInteger("menuItemID") < 0)
        {
            outlineMat.SetFloat("_OutlineIntensity", 2.5f);
            animator.SetInteger("menuItemID", animatorParam);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Rigidbody rigid;
    Animator anim;
    public bool isDoorCheckOn = false;
    public bool isBackDoorCheckOn= false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (isDoorCheckOn)
            FrontOpen();

        if(isBackDoorCheckOn)
            BackOpen();
    }

    public void FrontOpen()
    {
        StartCoroutine(OpenFrontDoor());
    }

    void BackOpen()
    {
        StartCoroutine (OpenBackDoor());
    }
    IEnumerator OpenFrontDoor()
    {
        anim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.1f);

        anim.SetBool("isOpen", false);
        isDoorCheckOn=false;

    }

    IEnumerator OpenBackDoor()
    {
        anim.SetBool("isBackOpen", true);
        yield return new WaitForSeconds(0.1f);

        anim.SetBool("isBackOpen", false);
        isBackDoorCheckOn = false;


    }
}

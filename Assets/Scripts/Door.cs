using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Rigidbody rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           StartCoroutine(OpenDoor());
        }
        StopCoroutine(OpenDoor());

    }

    IEnumerator OpenDoor()
    {
        anim.SetBool("isOpen", true);
        yield return new WaitForSeconds(0.1f);

        anim.SetBool("isOpen", false);

    }
}

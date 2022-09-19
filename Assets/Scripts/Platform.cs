using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Transform target;
    Rigidbody rigid;
    public float moveSpeed = 0.1f;
    Vector3 targetPosition;
    Vector3 originPosition;
    public bool isPlatformMoveOn = false;
    public bool isPlatformTop = false;
    


    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        targetPosition = target.transform.position;
        originPosition = new Vector3(17.81f, 0.18f,0);
    }


    IEnumerator OnMoveup()
    {
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        rigid.MovePosition(rigid.position + moveSpeed * Time.deltaTime * (targetPosition - rigid.position).normalized);
        yield return new WaitForSeconds(3f);
        isPlatformMoveOn = false;
        isPlatformTop = true;
    }

    IEnumerator OnMoveDown()
    {
        //transform.position = Vector3.MoveTowards(transform.position, originPosition, moveSpeed * Time.deltaTime);
        rigid.MovePosition(rigid.position + moveSpeed * Time.deltaTime * (originPosition - rigid.position).normalized);
        yield return new WaitForSeconds(3f);
        isPlatformMoveOn = false;
        isPlatformTop =false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isPlatformMoveOn && !isPlatformTop)
        {
            StartCoroutine(OnMoveup());
        }
        else if (other.gameObject.CompareTag("Player") && isPlatformMoveOn && isPlatformTop)
        {
            StartCoroutine(OnMoveDown());
        }

    }
}

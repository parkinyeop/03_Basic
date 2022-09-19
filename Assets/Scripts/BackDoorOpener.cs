using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackDoorOpener : MonoBehaviour
{
    Door doorParent;
    // Start is called before the first frame update
    void Start()
    {
        GameObject door = GameObject.Find("Door");
        doorParent = door.GetComponent<Door>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            doorParent.isBackDoorCheckOn = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoorOpener : MonoBehaviour
{
    Door doorParent;
    // Start is called before the first frame update
    void Start()
    {
        GameObject door = GameObject.Find("Door");
        doorParent = door.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            doorParent.isDoorCheckOn = true;
    }
}

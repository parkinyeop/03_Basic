using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    GameObject player;
    Vector3 originPos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = originPos+player.transform.position;
    }
}

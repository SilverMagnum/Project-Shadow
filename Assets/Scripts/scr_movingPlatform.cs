using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_movingPlatform : MonoBehaviour {

    public GameObject pointA;
    public Vector3 destination;
    public Vector3 newDestination;
    public float speed;
    public float waitTimer;
    public float waitCounter;

    void Start()
    {

        destination = pointA.transform.position;

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            other.gameObject.transform.parent = gameObject.transform;

        }

    }

    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            other.gameObject.transform.parent = null;

        }

    }

    void Update()
    {

        if(waitCounter > 0)
        {

            waitCounter -= 1;

        }

        if(waitCounter <= 0)
        {

            destination = newDestination;

        }

        transform.position = Vector3.MoveTowards(transform.position, destination, speed);

    }

}

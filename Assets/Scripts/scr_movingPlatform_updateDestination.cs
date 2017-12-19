using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_movingPlatform_updateDestination : MonoBehaviour {

    public GameObject platform;
    public GameObject nextStop;

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject == platform)
        {

            platform.GetComponent<scr_movingPlatform>().waitCounter = platform.GetComponent<scr_movingPlatform>().waitTimer;
            platform.GetComponent<scr_movingPlatform>().newDestination = nextStop.transform.position;

        }
        

    }
}

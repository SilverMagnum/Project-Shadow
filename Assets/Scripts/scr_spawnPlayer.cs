using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_spawnPlayer : MonoBehaviour {

    public GameObject player;
    public GameObject playerShoulder;

	// Use this for initialization
	void Start () {

        player = Instantiate<GameObject>(player, transform);
        playerShoulder = Instantiate<GameObject>(playerShoulder, transform);
        player.GetComponent<scr_playerControl>().shoulder = playerShoulder;
        player.GetComponent<scr_playerControl>().hand = playerShoulder.transform.Find("Hand").gameObject;
        player.GetComponent<scr_playerControl>().cam = playerShoulder.transform.Find("Main Camera").gameObject.GetComponent<Camera>();

        player.transform.parent = null;
        playerShoulder.transform.parent = null;

	}
	
}

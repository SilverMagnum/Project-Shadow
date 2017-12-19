using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_doorOpen : MonoBehaviour {

    public Animator anim;
    public AnimationClip open;
    public bool opened;

	// Use this for initialization
	void Start () {

		anim = gameObject.GetComponent<Animator>();

	}

}

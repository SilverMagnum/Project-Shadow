using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_playerControl : MonoBehaviour {

    public float moveSpeed;
    public float sprintMagnitude;
    public float jumpForce;
    public float grabRange;
    public float throwForce;
    public float jumpAlarm;
    float jumpTimer;
    public bool sprinting;
    public bool grounded;
    public bool grabbing;
    public bool isCrouching;
    bool isJumping;
    public Camera cam;
    public GameObject shoulder;
    public GameObject body;
    public GameObject hand;
    public GameObject grabbedObject;
    public Rigidbody movement;
    public RaycastHit checkGrabbable;
    Animator anim;
    public AnimationClip idle;
    public AnimationClip walk;

    void Start () {

        movement = gameObject.GetComponent<Rigidbody>();
        anim = gameObject.GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        sprinting = false;
        grounded = false;
        isCrouching = false;

    }

    private void OnTriggerStay(Collider other)
    {

        grounded = true;
        if(jumpTimer <= 0)
        {

            isJumping = false;

        }

    }

    private void OnTriggerExit(Collider other)
    {

        grounded = false;
        if(isJumping == false)
        {

             movement.velocity = new Vector3(movement.velocity.x, -jumpForce, movement.velocity.z);

        }

    }

    void Update () {

        //Gather Mouse or Right Stick Input
        var measureAim = new Vector3();
        measureAim.x = -Input.GetAxisRaw("Look Vertical");
        measureAim.y = Input.GetAxisRaw("Look Horizontal");
        measureAim.z = 0f;

        //Rotate Camera
        if (measureAim != Vector3.zero)
        {

            shoulder.transform.localRotation = Quaternion.Euler(shoulder.transform.localEulerAngles.x, shoulder.transform.localEulerAngles.y + measureAim.y, 0);

        }
        
        //Gather WASD or Left Stick Input
        float h = Input.GetAxisRaw("Move Horizontal");
        float v = Input.GetAxisRaw("Move Vertical");

        //Get Sprint Input and Decide if Player Should Sprint
        if(Input.GetButtonDown("Sprint") && grounded == true)
        {

            if(sprinting == false)
            {

                sprinting = true;

            }
            else if (sprinting == true)
            {

                sprinting = false;

            }

        }

        //Get Crouch Input
        if(Input.GetButtonDown("Crouch") && grounded == true)
        {

            if(isCrouching == false)
            {
                
                transform.localScale = new Vector3(1, 0.5f, 1);
                isCrouching = true;

            }
            else if(isCrouching == true)
            {
                
                transform.localScale = new Vector3(1, 1, 1);
                isCrouching = false;

            }

        }

        if ((h == 0 && v == 0) || isCrouching == true)
        {

            sprinting = false;

        }

        //Calculate Forward and Right Vectors
        Vector3 forward = shoulder.transform.forward;
        forward.y = 0f;
        forward = forward.normalized;
        Vector3 right = new Vector3(forward.z, 0.0f, -forward.x);

        //Calculate Desired Velocity
        var move = Vector3.zero;

        if (sprinting == false)
        {

            move = (h * right + v * forward).normalized * moveSpeed;

        }
        else if (sprinting == true)
        {

            move = ((h * right + v * forward).normalized * moveSpeed) * sprintMagnitude;

        }

        //Check if jumping
        if(jumpTimer > 0)
        {

            jumpTimer -= 1;

        }
        else
        {

            isJumping = false;
            jumpTimer = 0;

        }

        if (grounded == true && Input.GetButtonDown("Jump") && isCrouching == false)
        {
            jumpTimer = jumpAlarm;
            movement.velocity = new Vector3(movement.velocity.x, jumpForce, movement.velocity.z);
            isJumping = true;


        }

        movement.velocity = new Vector3(move.x, movement.velocity.y, move.z);

        //Check if Grab Button Pressed and an Object is Held
        if (Input.GetButtonDown("Grab/Drop") && grabbedObject != null)
        {

            //Drop Object
            grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbedObject.transform.parent = null;
            grabbedObject.transform.localScale = new Vector3(1, 1, 1);
            grabbedObject = null;

        }
        //Check if Grab Button Pressed
        else if (Input.GetButtonDown("Grab/Drop") && Physics.Raycast(shoulder.transform.position, shoulder.transform.forward, out checkGrabbable, grabRange) && grabbedObject == null)
        {

            //Grab Object
            if(checkGrabbable.collider.gameObject.tag == "grabbable")
            {

                grabbedObject = checkGrabbable.collider.gameObject;
                grabbedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                grabbedObject.transform.parent = shoulder.transform;
                grabbedObject.transform.localRotation = Quaternion.Euler(0, 180, 0);
                grabbedObject.transform.localPosition = hand.transform.localPosition;
                grabbedObject.transform.localScale = new Vector3(1, 1, 1);

            }

        }

        //Check if Interact Button Pressed
        if (Input.GetButtonDown("Interact") && Physics.Raycast(shoulder.transform.position, shoulder.transform.forward, out checkGrabbable, grabRange))
        {

            //Open Door
            if (checkGrabbable.collider.gameObject.tag == "door" && checkGrabbable.collider.gameObject.GetComponent<scr_doorOpen>().opened == false)
            {

                checkGrabbable.collider.gameObject.GetComponent<scr_doorOpen>().anim.Play(checkGrabbable.collider.gameObject.GetComponent<scr_doorOpen>().open.name);

            }

        }

        //Check if Throw Button Pressed and Object is Held
        if (Input.GetButtonDown("Throw") && grabbedObject != null)
        {

            //Throw Object
            grabbedObject.transform.position = shoulder.transform.position;
            grabbedObject.transform.rotation = shoulder.transform.rotation;
            grabbedObject.GetComponent<Rigidbody>().AddForce(shoulder.transform.forward * throwForce,ForceMode.VelocityChange);
            grabbedObject.transform.parent = null;
            grabbedObject.transform.localScale = new Vector3(1, 1, 1);
            grabbedObject = null;

        }

        //Check if an Object is Held
        if (grabbedObject != null)
        {

            //Update Grabbed Object Transform
            grabbedObject.transform.localRotation = hand.transform.localRotation;
            grabbedObject.transform.localPosition = hand.transform.localPosition;
            grabbedObject.transform.localScale = new Vector3(1, 1, 1);

        }

        //Adjust Body Transforms
        if (isCrouching == true)
        {

            shoulder.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.44f, gameObject.transform.position.z);

        }
        else if (isCrouching == false)
        {

            shoulder.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.887f, gameObject.transform.position.z);

        }

        //Find and Play the Correct nimation
        if (grounded == true)
        {
            if (move != Vector3.zero)
            {

                body.transform.LookAt(transform.position + move);
                anim.Play(walk.name);

            }
            else
            {

                anim.Play(idle.name);

            }
        }

    }
}

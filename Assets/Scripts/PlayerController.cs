using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Animator playerAnimator;
    public GameObject cam;

    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    public float movementSpd = 0.5f;

    void Start() {
        rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        //get horizontal and vertical axis positions
        horizontal = Input.GetAxisRaw("Horizontal") * movementSpd;
        vertical = Input.GetAxisRaw("Vertical") * movementSpd;

        //play animations based on player button input 
        if (Input.GetButtonDown("Horizontal"))
        {
            //play right animation when x is positive
            if (horizontal > 0)
            {
                playerAnimator.SetFloat("RightSpd", horizontal);
            }
            else //play left animation when x is negative
            {
                playerAnimator.SetFloat("LeftSpd", horizontal * (-movementSpd));
            }
        }

        //stop animations when button is released
        if (Input.GetButtonUp("Horizontal"))
        {
            playerAnimator.SetFloat("RightSpd", 0);
            playerAnimator.SetFloat("LeftSpd", 0);
        }

        //play animations based on player button input
        if (Input.GetButtonDown("Vertical"))
        {
            //play up animation when y is positive
            if (vertical > 0)
            {
                playerAnimator.SetFloat("UpSpd", vertical);
            }
            else //play down animation when y is negative
            {
                playerAnimator.SetFloat("DownSpd", vertical * (-movementSpd));
            }
        }

        //stop animations when button is released
        if (Input.GetButtonUp("Vertical"))
        {
            playerAnimator.SetFloat("UpSpd", 0);
            playerAnimator.SetFloat("DownSpd", 0);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal, vertical);    
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Debug.Log("GameObject1 collided with " + col.name);
        cam.GetComponent<CameraZoom>().updateNumTriggers(1, col.transform);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        // Debug.Log("GameObject1 collided with " + col.name);
        cam.GetComponent<CameraZoom>().updateNumTriggers(-1, col.transform);
    }
}
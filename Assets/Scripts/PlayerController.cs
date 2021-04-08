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
        horizontal = Input.GetAxisRaw("Horizontal") * movementSpd;
        vertical = Input.GetAxisRaw("Vertical") * movementSpd;

        if (Input.GetButtonDown("Horizontal"))
        {
            if (horizontal > 0)
            {
                playerAnimator.SetFloat("RightSpd", horizontal);
                //transform.position = transform.position + new Vector3(movementSpd, 0, 0);
                //transform.Translate(movementSpd, 0, 0);
            }
            else
            {
                playerAnimator.SetFloat("LeftSpd", horizontal * (-movementSpd));
                //transform.position = transform.position + new Vector3(-movementSpd, 0, 0);
                //transform.Translate(-movementSpd, 0, 0);
            }
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            playerAnimator.SetFloat("RightSpd", 0);
            playerAnimator.SetFloat("LeftSpd", 0);
        }

        if (Input.GetButtonDown("Vertical"))
        {
            if (vertical > 0)
            {
                playerAnimator.SetFloat("UpSpd", vertical);
                //transform.position = transform.position + new Vector3(0, movementSpd, 0);
                //transform.Translate(0, movementSpd, 0);
            }
            else
            {
                playerAnimator.SetFloat("DownSpd", vertical * (-movementSpd));
                //transform.position = transform.position + new Vector3(0, -movementSpd, 0);
                //transform.Translate(0, -movementSpd, 0);

            }
        }

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

//     public GameObject player;
//     public Animator playerAnimator;

//     private float horizontal;
//     private float vertical;
//     public float movementSpd = 0.5f;

//     // Update is called once per frame
//     void Update()
//     {
//         //get axis values
//         horizontal = Input.GetAxisRaw("Horizontal");
//         vertical = Input.GetAxisRaw("Vertical");

//         if (Input.GetButtonDown("Horizontal"))
//         {
//             if (horizontal > 0)
//             {
//                 playerAnimator.SetFloat("RightSpd", horizontal);
//                 //transform.position = transform.position + new Vector3(movementSpd, 0, 0);
//                 transform.Translate(movementSpd, 0, 0);
//             }
//             else
//             {
//                 playerAnimator.SetFloat("LeftSpd", horizontal * (-movementSpd));
//                 //transform.position = transform.position + new Vector3(-movementSpd, 0, 0);
//                 transform.Translate(-movementSpd, 0, 0);
//             }
//         }

//         if (Input.GetButtonUp("Horizontal"))
//         {
//             playerAnimator.SetFloat("RightSpd", 0);
//             playerAnimator.SetFloat("LeftSpd", 0);
//         }

//         if (Input.GetButtonDown("Vertical"))
//         {
//             if (vertical > 0)
//             {
//                 playerAnimator.SetFloat("UpSpd", vertical);
//                 //transform.position = transform.position + new Vector3(0, movementSpd, 0);
//                 transform.Translate(0, movementSpd, 0);
//             }
//             else
//             {
//                 playerAnimator.SetFloat("DownSpd", vertical * (-movementSpd));
//                 //transform.position = transform.position + new Vector3(0, -movementSpd, 0);
//                 transform.Translate(0, -movementSpd, 0);
//             }
//         }
        
//         if (Input.GetButtonUp("Vertical"))
//         {
//             playerAnimator.SetFloat("UpSpd", 0);
//             playerAnimator.SetFloat("DownSpd", 0);
//         }

//     }
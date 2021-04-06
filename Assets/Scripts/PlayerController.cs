using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public Animator playerAnimator;
    public float spd = 5f;

    private float horizontal;
    private float vertical;

    // Update is called once per frame
    void Update()
    {
        //get axis values
        horizontal = Input.GetAxisRaw("Horizontal") * spd;
        vertical = Input.GetAxisRaw("Vertical") * spd;

        //if negative horizonal value, move left
        if (horizontal < 0)
        {
            if (Input.GetButtonDown("Left"))
            {
                playerAnimator.SetFloat("LeftSpd", horizontal);
            }
        }
        //if positive horizonal value, move right
        else
        {
            if (Input.GetButtonDown("Right"))
            {
                playerAnimator.SetFloat("RightSpd", horizontal);
            }
        }

        //if negative vertical value, move down
        if (vertical < 0)
        {
            if (Input.GetButtonDown("Down"))
            {
                playerAnimator.SetFloat("DownSpd", vertical);
            }
        }
        //if positive vertical value, move up
        else
        {
            if (Input.GetButtonDown("Up"))
            {
                playerAnimator.SetFloat("UpSpd", vertical);
            }
        }

    }
}

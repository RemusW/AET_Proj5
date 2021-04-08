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
    }

    private void FixedUpdate() {
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

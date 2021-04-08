using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create a rectangle that the other GameObject will collide with.
// Note that this GameObject has no visibility.
// (View in the Scene view to see this GameObject.)

public class createBox : MonoBehaviour
{
    int id = 0;

    void Start()
    {
        // BoxCollider2D bc;
        // bc = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
        // bc.size = new Vector2(3.0f, 1.0f);
        // bc.isTrigger = true;

        // gameObject.transform.localScale = new Vector3(3.0f, 1.0f, 1.0f);
        // gameObject.transform.position = new Vector3(0.0f, -2.0f, 0.0f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("GameObject2 collided with " + col.name);
    }

    void createBound(int x, int y, float radius) {
        string name = "box" + id;
        GameObject box = new GameObject(name, typeof(BoxCollider2D));
        BoxCollider2D bc = box.GetComponent<BoxCollider2D>();
        // bc.size = new Vector2(x, y);
        bc.isTrigger = true;
        box.transform.localScale = new Vector3(radius, radius, 1);
        box.transform.position = new Vector3(x, y, 0);
        box.transform.parent = this.gameObject.transform;
        ++id;
    }
}
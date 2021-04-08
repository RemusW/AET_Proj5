using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform target;
    public float Speed;
    public float zoomScale = 0.6f;
    private bool zoomActive = true;
    private Camera cam;
    // stores the positions of the bounding boxes we have are in
    private List<Transform> q;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        q = new List<Transform>();
    }

    public void Update()
    {
        int largestIndex = q.Count-1;
        if (zoomActive)
        {
            // cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, Speed);
            cam.orthographicSize = 3;
            // cam.transform.position = transform.position;
        }
        else
        {
            // cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, Speed);
            for(int i=0; i<q.Count; ++i) {
                if(q[i].localScale.x > q[largestIndex].localScale.x)
                    largestIndex = i;
            }
            cam.orthographicSize = zoomScale * q[largestIndex].localScale.x;
            // cam.transform.position = Vector3.Lerp(cam.transform.position, target.position.y, Speed);
        }
        // if there are no triggers, track the player
        if(q.Count <= 0)
            transform.position = new Vector3(target.position.x, target.position.y, -10);
        // set position to the latest trigger position
        else
            transform.position = new Vector3(q[largestIndex].position.x, q[largestIndex].position.y, -10);
    }

    public void updateNumTriggers(int n, Transform trans) {
        // we entered a trigger, add it to the back
        if(n==1) {
            q.Add(trans);
            zoomActive = false;
        }
        else {
            q.Remove(trans);
            // if the list is empty, that means we should zoom back in on hte player
            if (q.Count <= 0)
                zoomActive = true;
            else
                zoomActive = false;
        }
        Debug.Log(q.Count + " " + zoomActive);
    }
}

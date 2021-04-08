using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public bool zoomActive;
    public Vector3[] Target;
    public Camera cam;
    public float Speed;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    public void LateUpdate()
    {
        if (zoomActive)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 3, Speed);
            cam.transform.position = Vector3.Lerp(cam.transform.position, Target[1],Speed);
        }
        else
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 5, Speed);
            cam.transform.position = Vector3.Lerp(cam.transform.position, Target[0], Speed);

        }
    }
}

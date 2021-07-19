using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camFitContent : MonoBehaviour
{
    public Vector2 halfSize;
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        if(cam.orthographicSize*cam.aspect<halfSize.x){
            Camera.main.orthographicSize = halfSize.x / cam.aspect;
        }
    }
}

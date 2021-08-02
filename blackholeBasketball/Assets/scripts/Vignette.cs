using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vignette : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;
        transform.localScale= new Vector2(2*cam.orthographicSize*((float)Screen.width/(float)Screen.height),2*cam.orthographicSize);
        
    }
}

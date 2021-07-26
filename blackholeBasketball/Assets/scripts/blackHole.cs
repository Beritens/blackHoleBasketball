using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackHole : MonoBehaviour
{
    public float Mass;
    public Renderer distortion;
    public void init(int i){
        distortion.sortingOrder = 100+i;
        distortion.sharedMaterial.SetFloat("_radius",2.9866f/Camera.main.orthographicSize);
    }
}

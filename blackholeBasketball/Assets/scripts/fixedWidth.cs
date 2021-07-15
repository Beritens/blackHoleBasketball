using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fixedWidth : MonoBehaviour
{
     // halfWidth = (# of World Units you want for the fixed width screen size) / 2
     public float halfWidth = 7f;
 
     void Start () 
     {
         // Force fixed width
         Camera.main.orthographicSize = halfWidth / Camera.main.aspect;
     }
}

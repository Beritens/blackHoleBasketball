using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent Event;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="ball"){
            Event.Invoke();
        }
    }
}

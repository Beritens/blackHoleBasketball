using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class buttonClick : MonoBehaviour
{
    [SerializeField]
    float amount = 0.9f;
    [SerializeField]
    float time = 0.1f;
    Vector3 scale;
    public UnityEvent delayedEvent;
    void Start()
    {
         scale = transform.localScale;
    }
    public void click(){
       
        LeanTween.scale(gameObject,scale*amount,time).setOnComplete(()=>LeanTween.scale(gameObject,scale,time).setOnComplete(()=>delayedEvent.Invoke()));
    }
}

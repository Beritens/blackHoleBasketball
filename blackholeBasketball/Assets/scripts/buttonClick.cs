using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class buttonClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    float amount = 0.9f;
    [SerializeField]
    float time = 0.1f;
    Vector3 scale;
    //public UnityEvent delayedEvent;
    void Start()
    {
         scale = transform.localScale;
    }
    // public void click(){
       
    //     LeanTween.scale(gameObject,scale*amount,time).setOnComplete(()=>LeanTween.scale(gameObject,scale,time).setOnComplete(()=>delayedEvent.Invoke()));
    // }
    // void OnMouseDown()
    // {
    //     Debug.Log("test");
    // }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale=scale*amount;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.localScale=scale;
    }
}

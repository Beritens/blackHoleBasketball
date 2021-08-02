using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class swiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    //could maybe reuse page swiper but there is so much that is specific for pages... well here we go again


    public float width;
    float actualWidth;
    public int itemCount;
    public float ease;
    Vector3 start;
    Camera cam;
    Vector3 panelLocation;
    public delegate void MyDelegate(int i);
    public static MyDelegate OnSelect;
    int current;
    void Awake()
    {
        start = transform.position;
        actualWidth = width* GetComponentInParent<Canvas>().transform.localScale.x;
        panelLocation=transform.position;
    }
    void Start()
    {
        cam = Camera.main;
        current = 0;
        OnSelect(current);
    }

    public void OnDrag(PointerEventData eventData)
    {
        float difference = cam.ScreenToWorldPoint(eventData.pressPosition).x-cam.ScreenToWorldPoint(eventData.position).x;

        transform.position = panelLocation- new Vector3(difference,0,0);
        int nearest = Mathf.Clamp(Mathf.RoundToInt((-(transform.position.x-start.x))/actualWidth),0,itemCount-1);
        selection(nearest);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int nearest = Mathf.Clamp(Mathf.RoundToInt((-(transform.position.x-start.x))/actualWidth),0,itemCount-1);
        Vector3 newLocation = start - Vector3.right*nearest*actualWidth;
        StartCoroutine(SmoothMove(transform.position,newLocation,ease));
        panelLocation=newLocation;
    }
    void selection(int i){
        if(i!= current){
            current=i;
            OnSelect(i);
        }
    }

    //really just copied it. This could have been done so much better. Oh Brunnen
    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds){
        float t = 0f;
        while(t<=1.0f){
            t+=Time.deltaTime/seconds;
            transform.position = Vector3.Lerp(startpos,endpos,Mathf.SmoothStep(0f,1f,t));
            yield return null;
        }
    }
    public float GetWidth(){
        return actualWidth = width* GetComponentInParent<Canvas>().transform.localScale.x;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    Camera cam;
    public float percentThreshold = 0.2f;
    public float width;
    public float easing = 0.5f;
    int current = 0;
    public int pageCount;
    void Start()
    {
        panelLocation = transform.position;
        cam = Camera.main;
    }
    public void OnDrag(PointerEventData eventData)
    {
        
        float difference = cam.ScreenToWorldPoint(eventData.pressPosition).x-cam.ScreenToWorldPoint(eventData.position).x;
        transform.position = panelLocation- new Vector3(difference,0,0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x-eventData.position.x)/Screen.width;
        if(Mathf.Abs(percentage)>=percentThreshold){
            Vector3 newLocation = panelLocation;
            if(percentage>0 && current<pageCount-1){
                newLocation -= Vector3.right * (width);
                current++;
            }
            else if(percentage<0 && current>0){ 
                newLocation += Vector3.right * (width);
                current--;
            }
            StartCoroutine(SmoothMove(transform.position,newLocation,easing));
            
            panelLocation = newLocation;
        }
        else{
            StartCoroutine(SmoothMove(transform.position,panelLocation,easing));
        }
        
    }

    

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds){
        float t = 0f;
        while(t<=1.0f){
            t+=Time.deltaTime/seconds;
            transform.position = Vector3.Lerp(startpos,endpos,Mathf.SmoothStep(0f,1f,t));
            yield return null;
        }
    }
}

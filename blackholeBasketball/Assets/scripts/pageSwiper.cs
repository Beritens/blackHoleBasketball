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
    public Transform pageHolder;
    public Transform otherPageStuffHolder;
    float actualWidth;
    public LevelSelect levelSelect;
    Vector3 startPos;
    void Awake()
    {
        startPos=transform.position;
        actualWidth = width* GetComponentInParent<Canvas>().transform.localScale.x;
    }

    void Start()
    {
        pageCount=levelSelect.stages.Length;
        panelLocation = transform.position;
        cam = Camera.main;
        scale();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        
        float difference = cam.ScreenToWorldPoint(eventData.pressPosition).x-cam.ScreenToWorldPoint(eventData.position).x;

        transform.position = panelLocation- new Vector3(difference,0,0);
        scale();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float percentage = (eventData.pressPosition.x-eventData.position.x)/Screen.width;
        if(Mathf.Abs(percentage)>=percentThreshold){
            Vector3 newLocation = panelLocation;
            if(percentage>0 && current<pageCount-1){
                newLocation -= Vector3.right * (actualWidth);
                current++;
            }
            else if(percentage<0 && current>0){ 
                newLocation += Vector3.right * (actualWidth);
                current--;
            }
            StartCoroutine(SmoothMove(transform.position,newLocation,easing));
            
            panelLocation = newLocation;
        }
        else{
            StartCoroutine(SmoothMove(transform.position,panelLocation,easing));
        }
        
    }
    void scale(){
        updateScale(current);
        updateScale(current-1);
        updateScale(current+1);
    }
    void updateScale(int index){
        if(index<0 || index >= pageHolder.childCount){
            return;
        }
        Transform page = pageHolder.GetChild(index);
        float dist = Mathf.Abs(page.position.x);
        dist= Mathf.Lerp(0.75f,1.25f,1-Mathf.Clamp01(dist*0.4f));
        page.localScale = Vector3.one *dist;
        otherPageStuffHolder.GetChild(index).localScale=page.localScale;
    }
    public void GoToPage(int index){
        current = index;
        transform.position= startPos-Vector3.right*(actualWidth)*current;
        panelLocation=transform.position;
        scale();
    }

    

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds){
        float t = 0f;
        while(t<=1.0f){
            t+=Time.deltaTime/seconds;
            transform.position = Vector3.Lerp(startpos,endpos,Mathf.SmoothStep(0f,1f,t));
            scale();
            yield return null;
        }
    }
}

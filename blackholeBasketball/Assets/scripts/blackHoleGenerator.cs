using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class blackHoleGenerator : MonoBehaviour
{
    public GameObject blackHolePrefab;
    blackHole currentBlackHole;
    Vector2 offset;
    public bool editing;
    List<Vector2> blackHolePositions = new List<Vector2>();
    
    List<blackHole> blackHoles = new List<blackHole>();
    [SerializeField]
    LayerMask blackHoleLayer;
    bool delete = false;
    int blackHoleCount = 0;
    [SerializeField]
    displayCount display;
    // Start is called before the first frame update
    void Start()
    {
        newLevel();
    }
    public void reset(){
        
        editing = false;
        blackHoles = new List<blackHole>();
        SpawnHoles();
    }
    public void newLevel(){
        editing = true;
        blackHoles = new List<blackHole>();
        blackHolePositions = new List<Vector2>();
        GetHoleCount();
        UpdateDisplay();
    }
    void GetHoleCount(){
        blackHoleCount = GameObject.FindObjectOfType<Level>().blackHoleCount;
    }
    

    // Update is called once per frame
    void Update()
    {
        if(editing)
        {
            placeAndMove();
        }
        
    }
    void placeAndMove(){
        
        if(Input.touches.Length > 0){
            
            Touch touch = Input.touches[0];
            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(touch.position);
            if(isMouseOverUI()){//EventSystem.current.IsPointerOverGameObject(0)){
                
                return;
            }
            if(touch.phase==TouchPhase.Began){
                currentBlackHole = GetBlackHole(mousePos);
                if(currentBlackHole != null){
                    //save offset of touch if finger is over black hole
                    offset = (Vector2)currentBlackHole.transform.position-mousePos;
                    
                    return;
                }
                else if(blackHoleCount-blackHoles.Count >= 1){
                    //spawn black hole
                    GameObject hole = GameObject.Instantiate(blackHolePrefab,mousePos,Quaternion.identity);
                    blackHolePositions.Add(mousePos);
                    
                    currentBlackHole = hole.GetComponent<blackHole>();
                    blackHoles.Add(currentBlackHole);
                    UpdateDisplay();
                }
            }
            else if(touch.phase==TouchPhase.Moved){
                if(currentBlackHole!= null){
                    //move black hole
                    currentBlackHole.transform.position = mousePos+offset;
                    blackHolePositions[blackHoles.IndexOf(currentBlackHole)]= currentBlackHole.transform.position;
                    
                }
            }
            else if(touch.phase == TouchPhase.Ended){
                if(currentBlackHole != null){
                    
                    if(delete){
                        blackHolePositions.RemoveAt(blackHoles.IndexOf(currentBlackHole));
                        blackHoles.Remove(currentBlackHole);
                        UpdateDisplay();
                        Destroy(currentBlackHole.gameObject);
                    }
                            
                        
                        
                    
                }
                
                
                currentBlackHole=null;
            }
        }
        
    }
    void SpawnHoles(){
        for(int i = 0; i<blackHolePositions.Count;i++){
            GameObject hole = GameObject.Instantiate(blackHolePrefab,blackHolePositions[i],Quaternion.identity);
            blackHoles.Add(hole.GetComponent<blackHole>());
            
        }
    }
    
    blackHole GetBlackHole(Vector2 pos){
        RaycastHit2D hit = Physics2D.Raycast(pos,Vector2.right,0,blackHoleLayer);
        if(hit.collider!= null){
            if(hit.collider.GetComponent<blackHole>()!= null){
                return hit.transform.GetComponent<blackHole>();
            }
        }
        return null;
    }
    bool isMouseOverUI(){
        delete = false;
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        bool over = false;
        for(int i = 0; i<raycastResultList.Count;i++){
            if(!(raycastResultList[i].gameObject.tag=="delete")){
                over = true;
            }
            else{
                delete = true;
            }
        }
        return over;
        
    }
    void UpdateDisplay(){
        display.Display(blackHoleCount-blackHoles.Count);
    }
    
}

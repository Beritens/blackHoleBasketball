using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 start;
    public Transform target;
    public float time;
    public float offset;
    public float rotSpeed;
    float startRot;
    float timeSinceStart;
    bool active = true;
    public GameObject movePreview;
    public Rigidbody2D rb;
    public bool physics =true;
    
    bool init = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
        start = transform.position;
        startRot = transform.rotation.eulerAngles.z;
        if(target != null){
            target.parent=null;
            SpawnMovePreview();
            SetPos(0);
        }
            
        Rotate(0);
        init = true;
    
    }
    void SpawnMovePreview(){
        GameObject prev = GameObject.Instantiate(movePreview,transform.position,Quaternion.identity);
        prev.transform.GetChild(0).position = start;
        prev.transform.GetChild(1).position = target.position;
        LineRenderer line=prev.transform.GetChild(2).GetComponent<LineRenderer>();
        line.SetPosition(0,start);
        line.SetPosition(1,target.position);
    }
    void OnEnable()
    {
        GameManager.OnStartEdit+= Stop;
    }
    void OnDisable()
    {
        GameManager.OnStartEdit-= Stop;
    }
    void Stop(){
        active = false;
        if(!init){
            Start();
        }
        if(target!= null)
            SetPos(0);
        Rotate(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(active){
            timeSinceStart+= Time.fixedDeltaTime;
            if(target!= null)
                SetPos(timeSinceStart);
            Rotate(timeSinceStart);
        }
        
    }
    void SetPos(float _time){
        float t = Mathf.PingPong(_time+offset,time);
        t/=time;
        if(!physics){
            transform.position= Vector2.Lerp(start,target.position,t);
            return;
        }
        rb.MovePosition(Vector2.Lerp(start,target.position,t));
        
    }
    void Rotate(float _time){
        
        if(!physics){
            transform.rotation=Quaternion.Euler(0,0,startRot+rotSpeed*_time);
            return;
        }
        rb.MoveRotation(startRot+rotSpeed*_time);
        
        
    }
}

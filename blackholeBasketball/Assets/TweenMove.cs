using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMove : MonoBehaviour
{
    [SerializeField]
    Vector2 dir;
    [SerializeField]
    float time;
    [SerializeField]
    float timeBack;
    Vector2 from= new Vector3();
    bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        from = transform.position;
        started = true;
    }

    public void Go(){
        
        LeanTween.move(gameObject,from+dir,time);
    }   
    public void Back(){
        if(!started){
            Start();
        }
        LeanTween.move(gameObject,from,timeBack);
    }
}

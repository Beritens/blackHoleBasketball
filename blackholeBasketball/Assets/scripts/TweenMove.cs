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
    bool there;
    // Start is called before the first frame update
    void Start()
    {
        from = transform.position;
        there = false;
    }

    public void Go(){
        if(!there){

            LeanTween.move(gameObject,from+dir,time);
            there = true;
        }
        
    }   
    public void Back(){
        if(there){
            LeanTween.move(gameObject,from,timeBack);
            there = false;
        }
        
    }
}

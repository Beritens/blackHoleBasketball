using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoTo : MonoBehaviour
{
    
    public void Move(Transform target){
       
        StartCoroutine(smooth(transform.position,new Vector3(target.position.x,target.position.y,-20),0.5f));
    }
    IEnumerator smooth(Vector3 start, Vector3 end, float time){
        float t = 0f;
        while(t<=1.0f){
            t+=Time.deltaTime/time;
            transform.position = Vector3.Lerp(start,end,Mathf.SmoothStep(0f,1f,t));
            yield return null;
        }
    }
}

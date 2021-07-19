using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballTracker : MonoBehaviour
{
    // [SerializeField]
    // float time;
    // float t= 0;
    bool playing = false;
    [SerializeField]
    Transform container;
    [SerializeField]
    GameObject point;
    [SerializeField]
    float minDist;
    List<Vector2> points = new List<Vector2>();
    Transform ball;
    
    Coroutine co;


    // Start is called before the first frame update
    void OnEnable()
    {
        GameManager.OnStartEdit+= startEditMode;
        GameManager.OnStartPlay+= startPlayMode;
    }
    void OnDisable()
    {
        GameManager.OnStartEdit-= startEditMode;
        GameManager.OnStartPlay-= startPlayMode;
    }

    // Update is called once per frame
    void Update()
    {
        if(playing){
            if(points.Count>1){
                if((points[points.Count-1]-(Vector2)ball.position).magnitude>minDist){
                    points.Add((Vector2)ball.position);
                }
            }
            else{
                points.Add((Vector2)ball.position);
            }
            // t+= Time.deltaTime;
            // if(t>= time){
            //     if(points.Count>1){
            //         if((points[points.Count-1]-(Vector2)ball.position).magnitude>minDist){
            //             t= 0f;
            //             points.Add((Vector2)ball.position);
            //         }
            //     }
            //     else{
            //         t= 0f;
            //         points.Add((Vector2)ball.position);
            //     }
                
            // }
        }
    }
    void startPlayMode(){
        if(co!= null)
            StopCoroutine(co);
        ball = GameManager.instance.ball.transform;
        playing = true;
        points = new List<Vector2>();
        //t= time;
        foreach (Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }
    
    void startEditMode(){
        playing = false;
        co = StartCoroutine(spawnPoints());
    }
    
    IEnumerator spawnPoints(){
        for(int i = 0; i<points.Count;i++){
            GameObject p = GameObject.Instantiate(point,points[i],Quaternion.identity,container);
            Vector3 s = p.transform.localScale;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boy : MonoBehaviour
{
    public Transform ball;
    public Transform IkRight;
    public Transform IkLeft;
    public Transform waist;
    public float distLeft;
    public float distRight;
    public bool jump;
    public float v0;
    Vector2 startLeft;
    Vector2 startRight;
    Vector2 offsetRight;
    Vector2 offsetLeft;
    float startWaist;
    bool right;
    bool left;
    basketBall basket;
    int wait;
    public Transform WinRightHand;
    public Transform WinLeftHand;
    bool won;
    float timeSinceWon= 0;

    // Start is called before the first frame update
    void Awake()
    {
        startLeft = IkLeft.position;
        startRight = IkRight.position;
        offsetLeft = startLeft-(Vector2)ball.position;
        offsetRight = startRight-(Vector2)ball.position;
        startWaist=waist.position.y;
        right = true;
        left = true;
        basket = ball.GetComponent<basketBall>();
        wait = basket.wait;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(left){
            if(((Vector2)ball.position+offsetLeft-startLeft).magnitude <distLeft){
                IkLeft.position=(Vector2)ball.position+offsetLeft;
            }
            else{
                left = false;
                IkLeft.position = startLeft+ ((Vector2)ball.position+offsetLeft-startLeft).normalized*distLeft;
                IkLeft.parent=waist;
        
            }
            
        }
        if(right){
            if(((Vector2)ball.position+offsetRight-startRight).magnitude <distRight){
                IkRight.position=(Vector2)ball.position+offsetRight;
            }
            else{
                right = false;
                IkRight.position = startRight+ ((Vector2)ball.position+offsetRight-startRight).normalized*distRight;
                IkRight.parent=waist;
            }
            
        }
        if(jump){
            float t = Time.timeSinceLevelLoad-Time.fixedDeltaTime*wait;
            waist.position = new Vector2(waist.position.x,Mathf.Max(startWaist,startWaist+v0*t-9.81f*t*t));
        }
        if(won){
            //hop up and down 
            float h = Mathf.Sin(timeSinceWon*6)*0.3f;
            if(h<0){
                h*=0.4f;
            }
            waist.position = new Vector2(waist.position.x,startWaist+h);
            timeSinceWon+= Time.deltaTime;
        }
            
        
    }
    void reset(){
        
        left = false;
        right = false;
        jump = false;
        IkRight.parent=transform;
        IkLeft.parent=transform;
        IkLeft.position=startLeft;
        IkRight.position=startRight;
        if(jump)
            waist.position = new Vector2(waist.position.x,startWaist);
        
    }
    void OnEnable()
    {
        GameManager.OnStartEdit+=reset;
        GameManager.OnWin+=Win;
    }
    
    void OnDisable()
    {
        GameManager.OnStartEdit-=reset;
        GameManager.OnWin-=Win;
    }
    void Win(){
        StartCoroutine(winning());
        won = true;
    }
    IEnumerator winning(){
        //move arms up 
        Vector2 startR = IkRight.position;
        Vector2 startL = IkLeft.position;
        for(float i=0; i<=1; i+= Time.deltaTime*6){
            IkRight.position = Vector2.Lerp(startR,WinRightHand.position,i);
            IkLeft.position = Vector2.Lerp(startL,WinLeftHand.position,i);
            yield return null;
        }

    }
}

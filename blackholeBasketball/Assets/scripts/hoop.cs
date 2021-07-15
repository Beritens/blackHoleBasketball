using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoop : MonoBehaviour
{
    bool over = false;
    public void overHoop(){
        over = true;
    }
    public void underHoop(){
        //only win if the ball hit the trigger above
        if(over){
            GameObject.FindObjectOfType<GameManager>().Win();
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSingelton : MonoBehaviour
{
    public static MusicSingelton instance;
    void Awake()
    {
        if(instance!=null && instance != this){
            Destroy(gameObject);
            return;
        }
        else{
            instance=this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}

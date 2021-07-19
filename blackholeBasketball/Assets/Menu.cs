using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void Open(){
        gameObject.SetActive(true);
        LeanTween.scale(gameObject,Vector3.one,0.2f).setDelay(1);
    }
    public void Close(){
        LeanTween.scale(gameObject,Vector3.zero,0.1f).setOnComplete(() =>gameObject.SetActive(false));
    }
}

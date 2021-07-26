using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [Scene]
    public string homeScene;
    public void Open(){
        gameObject.SetActive(true);
        LeanTween.scale(gameObject,Vector3.one,0.2f).setDelay(1);
    }
    public void Close(){
        LeanTween.scale(gameObject,Vector3.zero,0.1f).setOnComplete(() =>gameObject.SetActive(false));
    }
}

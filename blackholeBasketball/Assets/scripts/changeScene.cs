using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    [SerializeField][Scene]
    string scene;
    public void change(){
        SceneManager.LoadScene(scene);
    }
    public void changeDelay(float delay){
        StartCoroutine(GameManager.wait(delay,()=>SceneManager.LoadScene(scene)));
    }
}

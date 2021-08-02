using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Menu : MonoBehaviour
{
    //also shows black hole count

    [Scene]
    public string homeScene;
    int oldBlackHoleCount;
    public TextMeshProUGUI count;
    public void Open(){
        gameObject.SetActive(true);
        count.text= oldBlackHoleCount.ToString();
        LeanTween.scale(gameObject,Vector3.one,0.2f).setDelay(1).setOnComplete(()=>showBlackHoleCount());
    }
    public void Close(){
        LeanTween.scale(gameObject,Vector3.zero,0.1f).setOnComplete(() =>gameObject.SetActive(false));
    }
    void OnEnable()
    {
        GameManager.OnStartPlay+=GetOldBlackHoleCount;
    }
    void OnDisable()
    {
        GameManager.OnStartPlay-=GetOldBlackHoleCount;
    }
    void GetOldBlackHoleCount(){
        oldBlackHoleCount=PlayerPrefs.GetInt("blackHoles");
    }
    void showBlackHoleCount(){
        int newCount = PlayerPrefs.GetInt("blackHoles");
        StartCoroutine(showCount(oldBlackHoleCount,newCount));

    }
    IEnumerator showCount(int old, int newC){
        
        for(int i = 0; i<newC-old;i++){
            yield return new WaitForSeconds(0.1f);
            LeanTween.scale(count.gameObject,Vector3.one*1.2f,0.1f).setLoopPingPong(1);
            yield return new WaitForSeconds(0.1f);
            count.text= (old+(i+1)).ToString();
            
        }
    }
}

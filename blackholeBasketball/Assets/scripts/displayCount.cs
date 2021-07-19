using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayCount : MonoBehaviour
{
    [SerializeField]
    GameObject BHUI;
    [SerializeField]
    Transform container;
    [SerializeField]
    TextMeshProUGUI text;
    int prev = 0;
    public void Display(int count){
        
        text.text=count.ToString();
        LeanTween.scale(text.gameObject,Vector3.one*1.1f,0.1f).setLoopPingPong(1).setEase(LeanTweenType.easeOutQuad);
        for(int i = container.childCount-1;i>=0;i--) {
            if(i>count-1){
                GameObject g = container.GetChild(i).gameObject;
                LeanTween.scale(container.GetChild(i).gameObject,Vector3.zero,0.1f).setOnComplete(()=>GameObject.Destroy(g));

            }
            else{
                GameObject.Destroy(container.GetChild(i).gameObject);
            }
            
        }
        for(int i = 0;i<count; i++){
            Transform t = GameObject.Instantiate(BHUI,Vector2.zero,Quaternion.identity,container).transform;
            t.SetSiblingIndex(0);
            if(i>prev-1){
                t.SetSiblingIndex(i);
                t.localScale=Vector3.zero;
                LeanTween.scale(t.gameObject,Vector3.one,0.1f);
            }
            

        }
        prev = count;
    }
}

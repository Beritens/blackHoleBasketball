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
    public void Display(int count){
        text.text=count.ToString();
        foreach (Transform child in container) {
            GameObject.Destroy(child.gameObject);
        }
        for(int i = 0;i<count; i++){
            GameObject.Instantiate(BHUI,Vector2.zero,Quaternion.identity,container);
        }
    }
}

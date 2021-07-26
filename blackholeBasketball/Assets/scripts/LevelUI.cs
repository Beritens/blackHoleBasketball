using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public int stage;
    public int level;
    public LevelSelect levelSelect;
    public TextMeshProUGUI number;
    public Button button;


    public void load(){
        levelSelect.loadScene(stage,level);
        
    }
    public void init(int s, int l, LevelSelect sel, bool active){
        stage=s;
        level= l;
        levelSelect = sel;
        number.text = (l+1).ToString();
        button.interactable = active;
    }
    
}

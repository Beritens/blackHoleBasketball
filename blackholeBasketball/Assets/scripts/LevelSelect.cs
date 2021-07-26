using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    [System.Serializable]
    public class stage{
        public string name;
        [Scene]
        public string[] levels;
    }
    public stage[] stages;
    public Transform stageContainer;
    public GameObject page;
    public GameObject level;
    [Scene]
    public string introScene;
    void Awake()
    {
        if(!PlayerPrefs.HasKey("levels")){
            PlayerPrefs.SetInt("levels",-1);
            PlayerPrefs.SetInt("stages",0);
            SceneManager.LoadScene(introScene);
        }
    }
    void Start()
    {
        instantiatePages();
        
    }
    void instantiatePages(){
        for(int i = 0; i< stages.Length; i++){
            Transform p =stageContainer.GetChild(i);
            //get last children in page (levelContainer)
            p=p.GetChild(p.childCount-1);
            for(int j = 0; j< stages[i].levels.Length;j++){
                bool active = false;
                int currentStage = PlayerPrefs.GetInt("stages");
                if(i<currentStage){
                    active=true;
                }
                else if(i==currentStage){
                    active= PlayerPrefs.GetInt("levels")>=j-1;
                }
                else if(i== currentStage+1){
                    active= PlayerPrefs.GetInt("levels")>= stages[i-1].levels.Length-1 && j==0;
                }
                InitLevel(i,j,p,active);
            }
        }
    }
    void InitLevel(int s, int l,Transform page,bool a){
        LevelUI levelUI = GameObject.Instantiate(level,Vector2.zero,Quaternion.identity,page).GetComponent<LevelUI>();
        
        levelUI.init(s,l,this,a);
    }
    public void loadScene(int stage, int level){
        SceneManager.LoadScene(stages[stage].levels[level]);
    }
    public void loadScene(int levelIndex){
        
        int c = 0;
        for(int i = 0; i<stages.Length;i++){
            if(c+stages[i].levels.Length>levelIndex && levelIndex>=c){
                
                int stage = i;
                int level = levelIndex-c;
                loadScene(stage,level);
                return;
            }
            c+= stages[i].levels.Length;
            
        }
    }
    public void loadNext(){
        int stage = PlayerPrefs.GetInt("stages");
        if(PlayerPrefs.GetInt("levels")+1<=stages[stage].levels.Length){
            loadScene(stage,PlayerPrefs.GetInt("levels")+1);
        }
        else{
            loadScene(stage+1,0);
        }
        
    }
    [ContextMenu("reset")]
    public void Reset(){
        PlayerPrefs.DeleteAll();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{
    [System.Serializable]
    public class stage{
        public string name;
        public Sprite image;
        [Scene]
        public string[] levels;
    }
    public stage[] stages;
    public pageSwiper swiper;
    public Transform stageContainer;
    public GameObject page;
    public GameObject level;
    [Scene]
    public string introScene;
    void Awake()
    {
        if(!PlayerPrefs.HasKey("levels")){
            PlayerPrefs.SetInt("levels",-1);
            SceneManager.LoadScene(introScene);
        }
    }
    void Start()
    {
        swiper.pageCount = stages.Length;
        instantiatePages();
        
    }
    int levelIndex;
    void instantiatePages(){
        for(int i = 0; i< stages.Length; i++){
            Transform p = GameObject.Instantiate(page,Vector2.zero,Quaternion.identity,stageContainer).transform;
            for(int j = 0; j< stages[i].levels.Length;j++){
                InitLevel(i,j,p,levelIndex<=PlayerPrefs.GetInt("levels")+1);
                levelIndex++;
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
        loadScene(PlayerPrefs.GetInt("levels")+1);
    }
    [ContextMenu("reset")]
    public void Reset(){
        PlayerPrefs.DeleteAll();
    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    bool editPhase = false;
    public basketBall ball;
    public blackHoleGenerator blackHoleGenerator;
    public static GameManager instance = null;
    public delegate void MyDelegate();
    public static MyDelegate OnStartEdit;
    public static MyDelegate OnStartPlay;
    public static MyDelegate OnWin;
    public float floorHeight;
    bool won = false;
    bool newLevel = false;
    int level;
    [SerializeField]
    Menu menu;
    bool startEdit = false;
    
    
    
    // Start is called before the first frame update
    void Awake(){

         if(instance == null){
              instance = this;
              DontDestroyOnLoad(gameObject);
         }else if(instance != this){
              Destroy(this.gameObject);
              return;
         }
         SceneManager.sceneLoaded += OnSceneLoaded;
         
         
     }
     void OnDisable()
     {
         SceneManager.sceneLoaded -= OnSceneLoaded;
     }
     void Start()
     {
        ball = GameObject.FindObjectOfType<basketBall>();
        Input.multiTouchEnabled = false;
        //ball.DontStart();
        blackHoleGenerator.newLevel();
        OnStartPlay();
        //switchPhase();
     }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        if(GameObject.FindObjectOfType<Level>() == null){
            Destroy(gameObject);
            return;
        }
        won = false;
        //do this at the start of a level
        ball = GameObject.FindObjectOfType<basketBall>();
        menu.Close();
        if(newLevel){
            //delete all saved blackholes
            blackHoleGenerator.newLevel();
        }
        else{
            blackHoleGenerator.reset();
            
        }
        OnStartPlay();
        newLevel = false;
        level = GameObject.FindObjectOfType<Level>().levelNumber;
        
        if(startEdit){
            //start with edit phase (reset button on winScreen)
            startEditPhase();
            startEdit=false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //not used in the app
        if(Input.GetKeyDown(KeyCode.Space)){
            switchPhase();
        }
    }
    public void switchPhase(){
        if(won){
            return;
        }
            
        editPhase = !editPhase;
        blackHoleGenerator.editing=editPhase;
        if(editPhase){
            startEditPhase();
        }
        else{
            startPlayPhase();
        }
    }
    public void startEditPhase(){
        if(won){
            return;
        }
        editPhase = true;
        OnStartEdit();
        blackHoleGenerator.editing=editPhase;
    }
    public void startPlayPhase(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OnStartPlay();
    }
    public void Win(){
        if(won)
            return;
        won = true;
        if(PlayerPrefs.GetInt("levels")<level){
            PlayerPrefs.SetInt("levels",level);
        }
        OnWin();
        menu.Open();

        
    }
    public void GoToNextLevel(){
        newLevel = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void Reset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        startEdit=true;
    }
}

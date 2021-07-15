using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool editPhase = false;
    public basketBall ball;
    public blackHoleGenerator blackHoleGenerator;
    public static GameManager instance = null;
    public delegate void MyDelegate();
    public static MyDelegate OnStartEdit;
    public static MyDelegate OnWin;
    public float floorHeight;
    public GameObject WinMenu;
    bool newLevel = false;
    int level;
    
    
    
    
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
     void Start()
     {
         Input.multiTouchEnabled = false;
        ball.DontStart();
        blackHoleGenerator.newLevel();
        switchPhase();
     }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        //do this at the start of a level
        ball = GameObject.FindObjectOfType<basketBall>();
        WinMenu.SetActive(false);
        if(newLevel){
            ball.DontStart();
            blackHoleGenerator.newLevel();
            switchPhase();
        }
        else{
            blackHoleGenerator.reset();
            
        }
        newLevel = false;
        level = GameObject.FindObjectOfType<Level>().levelNumber;
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
        editPhase = !editPhase;
        blackHoleGenerator.editing=editPhase;
        if(editPhase){
            OnStartEdit();
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void Win(){
        if(PlayerPrefs.GetInt("levels")<level){
            PlayerPrefs.SetInt("levels",level);
        }
        OnWin();
        WinMenu.SetActive(true);
    }
    public void GoToNextLevel(){
        newLevel = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        //SceneManager.LoadScene(GameObject.FindObjectOfType<Level>().nextScene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class basektballStore : MonoBehaviour
{
    [System.Serializable]
    public class ballItem{
        public Sprite sprite;
        public string name;
        [TextArea]
        public string quirkyComment;
        public int cost;
    }
    public ballItem[] ballz;
    public Transform container;
    public swiper swiper;
    public GameObject ballDisplay;
    List<GameObject> ballObjects = new List<GameObject>();
    public TextMeshProUGUI NameText;
    int current = 0;
    public TextMeshProUGUI blackHoleCount;

    public GameObject NotBoughtWindow;
    public GameObject BoughtWindow;
    public GameObject SelectedWindow;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI comment;
    public GameObject notEnough;
    public Transform HighLight;


    void OnEnable()
    {
        swiper.OnSelect+=select;
    }
    void OnDisable()
    {
        swiper.OnSelect-=select;
    }
    void Awake()
    {
        SpawnStuff();
        swiper.itemCount=ballz.Length;
        PlayerPrefs.SetInt("ballBought0",1);
    }
    void Start()
    {
        updateBlackHoleCount();
    }
    void SpawnStuff(){
        for(int i = 0; i<ballz.Length;i++){
            GameObject b = GameManager.Instantiate(ballDisplay,Vector2.zero,Quaternion.identity,container);
            ballObjects.Add(b);
            b.GetComponent<Image>().sprite=ballz[i].sprite;
            b.transform.position=container.position+Vector3.right*swiper.GetWidth()*i;
        }
        updateSelected();
    }

    void select(int i){
        current=i;
        resetSize();
        Transform b = ballObjects[i].transform;
        b.localScale=Vector2.one*1.75f;
        b.SetAsLastSibling();
        

        NameText.text=ballz[i].name;
        
        closeAll();
        if(PlayerPrefs.GetInt("ball")==current){
            initSelectedWindow();
        }
        else if(PlayerPrefs.GetInt("ballBought"+current.ToString())==1){
            initBoughtWindow();
        }
        else{
            initNotBoughtWindow();
        }
        updateSelected();
    }
    void closeAll(){
        SelectedWindow.SetActive(false);
        BoughtWindow.SetActive(false);
        NotBoughtWindow.SetActive(false);
    }
    void initSelectedWindow(){
        closeAll();
        comment.text=ballz[current].quirkyComment;
        SelectedWindow.SetActive(true);
    }
    void initBoughtWindow(){
        closeAll();
        BoughtWindow.SetActive(true);
    }
    void initNotBoughtWindow(){
        closeAll();
        Price.text=ballz[current].cost.ToString();
        NotBoughtWindow.SetActive(true);
    }
    void resetSize(){
        foreach (GameObject item in ballObjects)
        {
            item.transform.localScale=Vector2.one;
        }
    }
    public void chooseBall(){
        PlayerPrefs.SetInt("ball",current);
        initSelectedWindow();
        updateSelected();
    }
    public void buyBall(){
        
        int bHoles = PlayerPrefs.GetInt("blackHoles");
        if(bHoles>= ballz[current].cost){
            bHoles-= ballz[current].cost;
            PlayerPrefs.SetInt("blackHoles",bHoles);
            updateBlackHoleCount();
            PlayerPrefs.SetInt("ballBought"+current.ToString(),1);
            PlayerPrefs.SetInt("ball",current);
            initSelectedWindow();
            updateSelected();
        }
        else{
            notEnough.SetActive(true);
            StartCoroutine(GameManager.wait(0.3f,()=>notEnough.SetActive(false)));
        }
        //maybe play a sound for more feedback
        
    }
    public void updateBlackHoleCount(){
        blackHoleCount.text=PlayerPrefs.GetInt("blackHoles").ToString();
    }
    void updateSelected(){
        int sel = PlayerPrefs.GetInt("ball");
        Transform b = ballObjects[sel].transform;
        HighLight.position=b.position;
        HighLight.localScale=b.localScale;
        HighLight.SetSiblingIndex(Mathf.Max(b.GetSiblingIndex()-1,0));
    }

}

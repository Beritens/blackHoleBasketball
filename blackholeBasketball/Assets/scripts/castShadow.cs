using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class castShadow : MonoBehaviour
{
    public GameObject shadowPrefab;
    Transform shadow;
    [SerializeField]
    float offset = 0;
    Vector2 startScale;
    [SerializeField]
    Vector2 size = Vector2.one;
    [SerializeField]
    bool resize = true;
    [SerializeField]
    Sprite shadowForm;
    // Start is called before the first frame update
    void Start()
    {
        spawnShadow();
        setShadowPosition();
    }
    void spawnShadow(){
        if(shadow != null){
            return;
        }
        else{
            shadow = GameObject.Instantiate(shadowPrefab,transform.position,Quaternion.identity).transform;
            if(shadowForm!=null)
                shadow.GetComponent<SpriteRenderer>().sprite=shadowForm;
            startScale = shadow.localScale;
            
        }
        
    }
    

    // Update is called once per frame
    void LateUpdate()
    {
        if(shadow==null){
            spawnShadow();
        }
        setShadowPosition();
    }
    void setShadowPosition(){
        float floorHeight = GameManager.instance!=null?GameManager.instance.floorHeight:-4;
        shadow.position = new Vector2(transform.position.x+offset,floorHeight);
        float changeby= resize?Mathf.Max(Mathf.Sqrt(transform.position.y+1-floorHeight),1):1;
        shadow.localScale = Vector2.Scale(startScale,size)/changeby;
    }
    void OnDrawGizmos()
    {
        float floorHeight = -4;
        float changeby= resize?Mathf.Max(Mathf.Sqrt(transform.position.y-floorHeight),1):1;
        Vector2 normalScale = new Vector2(1,0.28333f);
        Gizmos.DrawWireCube(new Vector2(transform.position.x+offset,floorHeight),new Vector3(normalScale.x*size.x,normalScale.y*size.y,0.5f)/changeby);
    }
}

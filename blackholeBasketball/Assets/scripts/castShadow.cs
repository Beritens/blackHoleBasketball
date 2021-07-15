using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castShadow : MonoBehaviour
{
    public GameObject shadowPrefab;
    Transform shadow;
    Vector2 startScale;
    [SerializeField]
    float size;
    // Start is called before the first frame update
    void Start()
    {
        shadow = GameObject.Instantiate(shadowPrefab,transform.position,Quaternion.identity).transform;
        shadow.localScale*=size;
        startScale = shadow.localScale;
        setShadowPosition();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        setShadowPosition();
    }
    void setShadowPosition(){
        shadow.position = new Vector2(transform.position.x,GameManager.instance.floorHeight);
        shadow.localScale = startScale/Mathf.Max(Mathf.Sqrt(transform.position.y-GameManager.instance.floorHeight),1);
    }
}

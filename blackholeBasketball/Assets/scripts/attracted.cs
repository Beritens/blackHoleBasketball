using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attracted : MonoBehaviour
{
    blackHole[] holes;
    Rigidbody2D rb;
    public static float G = 1;
    bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        holes = GameObject.FindObjectsOfType<blackHole>();
        rb = GetComponent<Rigidbody2D>();
    }
    void OnEnable()
    {
        GameManager.OnStartEdit+=stopIt;
    }
    
    void OnDisable()
    {
        GameManager.OnStartEdit-=stopIt;
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            holes = GameObject.FindObjectsOfType<blackHole>();
            
        }
    }
    void stopIt(){
        active = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!active){
            return;
        }
        foreach (blackHole hole in holes)
        {
            rb.AddForce((G*(rb.mass*hole.Mass)/Mathf.Pow(((Vector2)hole.transform.position-(Vector2)transform.position).magnitude,2))*((Vector2)hole.transform.position-(Vector2)transform.position).normalized,ForceMode2D.Force);
        }
        
    }
}

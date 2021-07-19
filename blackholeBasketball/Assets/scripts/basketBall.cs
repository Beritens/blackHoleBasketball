using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basketBall : MonoBehaviour
{
    Vector2 start;
    Rigidbody2D rb;
    bool starting = true;
    public Transform target;
    public float force;
    public int wait = 5;
    Transform orange;
    public AudioSource source;
    [SerializeField]
    AudioClip groundSound;
    [SerializeField]
    AudioClip HoopSound;
    [SerializeField]
    AudioClip whoosh;
    [SerializeField]
    AudioClip gone;
    [SerializeField]
    float vol;
    void Awake()
    {
        start = transform.position;
        rb= GetComponent<Rigidbody2D>();
        starting = true;
        orange = transform.GetChild(0);
    }
    public void DontStart(){
        starting = false;
    }
    void OnEnable()
    {
        GameManager.OnStartEdit+=reset;
    }
    
    void OnDisable()
    {
        GameManager.OnStartEdit-=reset;
    }
    public void reset(){
        gameObject.SetActive(true);
        starting = false;
        transform.position=start;
        transform.localRotation = Quaternion.identity;
        rb.isKinematic=false;
        rb.simulated= false;
        

    }
    public void throwBall(){
        rb.simulated = true;
        rb.isKinematic=false;
        rb.velocity=(target.position-transform.position).normalized*force;
        source.PlayOneShot(whoosh);
        
    }
    void FixedUpdate()
    {
        if(starting){
            if(wait <= 0){
                
                throwBall();
                starting = false;
            }
            wait--;
            
        }
        orange.rotation = Quaternion.identity;
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        float volume = Mathf.Clamp01(other.relativeVelocity.magnitude * vol);
        switch (other.gameObject.tag)
        {
            case "noSound":
                return;
            case "hoop":
                source.PlayOneShot(HoopSound,volume);
                break;
            case "blackHole":
                transform.position= Vector2.right*100;
                rb.simulated=false;
                source.PlayOneShot(gone,volume);
                break;
            default:
                source.PlayOneShot(groundSound,volume);
                break;
        }
    }

    
   

}

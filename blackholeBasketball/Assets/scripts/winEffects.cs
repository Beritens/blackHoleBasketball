using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winEffects : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] particles;
    [SerializeField]
    AudioSource source;
    [SerializeField]
    AudioClip scoreSound;
    void OnEnable()
    {
        GameManager.OnWin+=effects;
        GameManager.OnStartPlay+= reset;
    }
    void OnDisable()
    {
        GameManager.OnWin-=effects;
        GameManager.OnStartPlay -= reset;
    }
    void effects(){
        source.Play();
        source.PlayOneShot(scoreSound);
        foreach (ParticleSystem particle in particles)
        {
            particle.Play();
        }
    }
    void reset(){
        source.Stop();
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
            particle.Clear();
        }
    }
}

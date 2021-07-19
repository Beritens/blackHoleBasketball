using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField]
    TweenMove[] tweens;
    [SerializeField]
    TweenMove[] winStuff;
    void OnEnable()
    {
        GameManager.OnStartEdit+=StartEdit;
        GameManager.OnStartPlay+=StartPlay;
        GameManager.OnWin += Win;
    }
    void OnDisable()
    {
        GameManager.OnStartEdit-=StartEdit;
        GameManager.OnStartPlay-=StartPlay;
        GameManager.OnWin -= Win;
    }
    public void StartEdit(){
        foreach (TweenMove tween in tweens)
        {
            tween.Go();
        }
        foreach (TweenMove tween in winStuff)
        {
            tween.Back();
        }
    }
    public void StartPlay(){
        foreach (TweenMove tween in tweens)
        {
            tween.Back();
        }
        foreach (TweenMove tween in winStuff)
        {
            tween.Back();
        }
    }
    void Win(){
        foreach (TweenMove tween in winStuff)
        {
            tween.Go();
        }
    }
}

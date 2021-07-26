using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]
    TweenMove[] tweens;
    [SerializeField]
    TweenMove[] winStuff;
    [SerializeField]
    Image startButton;
    [SerializeField]
    Sprite play;
    [SerializeField]
    Sprite pause;
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
        startButton.sprite=play;

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
        startButton.sprite=pause;
    }
    void Win(){
        foreach (TweenMove tween in winStuff)
        {
            tween.Go();
        }
    }
}

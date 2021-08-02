using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skin : MonoBehaviour
{
    [System.Serializable]
    public class ballSkin{
        public Sprite topMoving;
        public Sprite notMoving;
        public Sprite bottomMoving;
    }
    public SpriteRenderer top;
    public SpriteRenderer mid;
    public SpriteRenderer bot;
    public ballSkin[] skins;
    void Start()
    {
        int i = PlayerPrefs.GetInt("ball");
        ballSkin s = skins[i];
        top.sprite=s.topMoving;
        mid.sprite=s.notMoving;
        bot.sprite=s.bottomMoving;
    }
}

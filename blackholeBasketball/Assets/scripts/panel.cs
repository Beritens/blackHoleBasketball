using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class panel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform t = GetComponent<RectTransform>();
        t.sizeDelta= new Vector2(t.sizeDelta.y*((float)Screen.width/(float)Screen.height),t.sizeDelta.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.EventSystems;

public class trash : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerUpHandler
{
    [SerializeField]
    Sprite closed;
    [SerializeField]
    Sprite open;
    [SerializeField]
    Image image;

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = closed;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if(eventData.pointerId<0)
            return;
        image.sprite = open;
    }


    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        image.sprite = closed;
    }
}

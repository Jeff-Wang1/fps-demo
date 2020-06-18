using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RunButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerControl._instance.isRunning = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerControl._instance.isRunning = false;
    }
}

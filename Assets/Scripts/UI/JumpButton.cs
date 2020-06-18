using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JumpButton : MonoBehaviour,IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerControl._instance.DoJump();
    }

}

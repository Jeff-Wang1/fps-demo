﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ReloadButton : MonoBehaviour,IPointerDownHandler
{

    public void OnPointerDown(PointerEventData eventData)
    {
        Weapon.instance.Reload();
    }

}

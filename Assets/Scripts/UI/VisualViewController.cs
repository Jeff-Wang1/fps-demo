using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VisualViewController : MonoBehaviour, IDragHandler,IPointerDownHandler
{
    private Vector2 initialTouchPosition;               //刚开始触摸的屏幕坐标
    public float smoothness = 0.05f;                    //拖拉视角灵敏度

    void IDragHandler.OnDrag(PointerEventData eventData)
    {

        Vector2 touchPositionDifference = eventData.position - initialTouchPosition;

        //改变视角方向
        PlayerControl._instance.DoChangeView(smoothness * touchPositionDifference.x, smoothness * touchPositionDifference.y);

        initialTouchPosition = eventData.position;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        initialTouchPosition = eventData.position;
    }

}

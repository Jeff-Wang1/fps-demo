using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VisualDirectionController : ScrollRect
{
    private float mRadius;
    private float smoothness = 1.5f;

    protected override void Start()
    {
        mRadius = this.GetComponent<RectTransform>().sizeDelta.x * 0.5f;
        content.gameObject.SetActive(false);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        content.gameObject.SetActive(true);

        //将触摸变得更灵敏
        Vector2 touchCirclePosition = RectTransformUtility.WorldToScreenPoint(null, content.position);
        Vector2 dif = eventData.position - touchCirclePosition;
        eventData.position = touchCirclePosition + dif * 2.5f;

        base.OnDrag(eventData);

        //虚拟摇杆移动
        var contentPostion = this.content.anchoredPosition;
        if (contentPostion.magnitude > mRadius)
        {
            contentPostion = contentPostion.normalized * mRadius;
            SetContentAnchoredPosition(contentPostion);
        }

        //人物移动
        var moveDirection = contentPostion.normalized;
        PlayerControl._instance.DoMove(moveDirection.x * smoothness, moveDirection.y * smoothness);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);

        content.gameObject.SetActive(false);

    }

}
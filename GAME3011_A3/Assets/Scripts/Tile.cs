using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum TileType
{
    Blue,
    Green,
    Orange,
    Purple,
    Red,
    Yellow
}

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public bool selected;
    public TileType type;
    CanvasGroup canvasGroup;

    RectTransform rectTransform;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        selected = true;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        selected = false;
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x < 0)
            {
                print("swipe left");
            }
            else if (eventData.delta.x > 0)
            {
                print("swipe right");
            }
        }

        else if(Mathf.Abs(eventData.delta.y) > Mathf.Abs(eventData.delta.x))
        {
            if (eventData.delta.y < 0)
            {
                print("swipe down");
            }
            else if (eventData.delta.y > 0)
            {
                print("swipe up");
            }
        }
    }
}

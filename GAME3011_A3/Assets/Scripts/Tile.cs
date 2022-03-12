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

public enum TileMovement
{
    None,
    Right,
    Left,
    Up,
    Down
}

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public bool selected;
    public TileType type;
    public TileMovement movement;
    CanvasGroup canvasGroup;

    RectTransform rectTransform;
    bool move = true;

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
                movement = TileMovement.Left;
            }
            else if (eventData.delta.x > 0)
            {
                print("swipe right");
                movement = TileMovement.Right;
            }
        }

        else if(Mathf.Abs(eventData.delta.y) > Mathf.Abs(eventData.delta.x))
        {
            if (eventData.delta.y < 0)
            {
                print("swipe down");
                movement = TileMovement.Down;
            }
            else if (eventData.delta.y > 0)
            {
                print("swipe up");
                movement = TileMovement.Up;
            }
        }
    }

    public bool CanMove()
    {
        return move;
    }

    public void CancelMove()
    {
        move = false;
    }

    public void RemoveParent()
    {
        transform.parent = transform.parent.parent;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
        transform.position = parent.position;
    }
}

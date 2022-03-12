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

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool selected;
    public TileType type;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
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
}

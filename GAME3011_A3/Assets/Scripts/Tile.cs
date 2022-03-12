using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool selected;
    Image tileSprite;
    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        tileSprite = GetComponent<Image>();
    }

    public void SetImage(Sprite newImage)
    {
        tileSprite.sprite = newImage;
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

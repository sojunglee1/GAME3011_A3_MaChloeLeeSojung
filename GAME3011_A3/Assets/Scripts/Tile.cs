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
    Canvas canvas;
    Animator animator;

    public bool OverrideSorting
    {
        get { return canvas.overrideSorting; }
        set { canvas.overrideSorting = value; }
    }
    public int SortingOrder
    {
        get { return canvas.sortingOrder; }
        set { canvas.sortingOrder = value; }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponent<Canvas>();
        animator = GetComponent<Animator>();
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
        OverrideSorting = true;
        SortingOrder = 1;
    }

    public void RemoveParent()
    {
        transform.SetParent(transform.parent.parent);
    }

    public void SetParent(Transform parent)
    {
        transform.SetParent(parent);
    }

    public void PlayAnimation(TileMovement movement)
    {
        switch (movement)
        {
            case TileMovement.Right:
                animator.Play("MoveRight");
                break;
            case TileMovement.Left:
                animator.Play("MoveLeft");
                break;
            case TileMovement.Up:
                animator.Play("MoveUp");
                break;
            case TileMovement.Down:
                animator.Play("MoveDown");
                break;
        }
    }
}

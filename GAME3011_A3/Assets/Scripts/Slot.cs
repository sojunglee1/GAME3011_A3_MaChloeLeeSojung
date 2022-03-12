using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Tile tile;
    public List<Tile> tileList;

    public Vector2 id;

    private void Awake()
    {
        Random.seed = Random.Range(0, 100);
    }

    private void Start()
    {
        CreateTile();
    }

    public void CreateTile()
    {
        int random = Random.Range(0, tileList.Count);
        Tile newItem = Instantiate(tileList[random], transform.position, Quaternion.identity, transform);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (CanSwitchTiles(eventData))
            {
                SwitchTiles(eventData);
            }
        }
    }

    public bool CanSwitchTiles(PointerEventData eventData)
    {
        bool move = false;
        if (transform.childCount.Equals(1) && eventData.pointerDrag.gameObject.GetComponent<Tile>().CanMove())
        {
            move = true;
        }
        return move;
    }

    public void SwitchTiles(PointerEventData eventData)
    {
        Tile tile1 = eventData.pointerDrag.GetComponent<Tile>();
        Tile tile2 = transform.GetChild(0).GetComponent<Tile>();

        Transform tile1Parent = eventData.pointerDrag.gameObject.GetComponent<Tile>().transform.parent;
        Transform tile2Parent = this.transform; 

        if (AdjacentTile(tile2, tile1))
        {
            tile1.SetParent(tile2Parent);
            tile2.SetParent(tile1Parent);
        }
    }
    
    public void SetID(Vector2 id)
    {
        this.id = id;
    }

    public Vector2 GetID()
    {
        return id;
    }

    public bool AdjacentTile(Tile currentTile, Tile adjacentTile)
    {
        Vector2 currentID = currentTile.transform.parent.GetComponent<Slot>().GetID();
        Vector2 adjacentID = adjacentTile.transform.parent.GetComponent<Slot>().GetID();

        bool x = ((currentID.x + 1).Equals(adjacentID.x) || (currentID.x - 1).Equals(adjacentID.x)) && (currentID.y.Equals(adjacentID.y));
        bool y = ((currentID.y + 1).Equals(adjacentID.y) || (currentID.y - 1).Equals(adjacentID.y)) && (currentID.x.Equals(adjacentID.x));

        if (x || y)
        {
            return true;
        }
        else return false;
    }
}

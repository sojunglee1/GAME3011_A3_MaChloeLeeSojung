using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Tile tile;
    public List<Tile> tileList;

    public Vector2 id;
    public Vector2 ID
    {
        get { return id; }
        set { id = value; }
    }

    [System.Obsolete]
    private void Start()
    {
        Random.seed = Random.Range(0, 100);
    }

    public void CreateTile()
    {
        int random = Random.Range(0, tileList.Count);
        var newTile = Instantiate(tileList[random], transform.position, Quaternion.identity, transform);
        tile = newTile;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (transform.childCount.Equals(1))
            {
                SwitchTiles(eventData);
            }
            
        }
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

    public bool AdjacentTile(Tile currentTile, Tile adjacentTile)
    {
        Vector2 currentID = currentTile.transform.parent.GetComponent<Slot>().ID;
        Vector2 adjacentID = adjacentTile.transform.parent.GetComponent<Slot>().ID;

        bool x = ((currentID.x + 1).Equals(adjacentID.x) || (currentID.x - 1).Equals(adjacentID.x)) && (currentID.y.Equals(adjacentID.y));
        bool y = ((currentID.y + 1).Equals(adjacentID.y) || (currentID.y - 1).Equals(adjacentID.y)) && (currentID.x.Equals(adjacentID.x));

        if (x || y)
        {
            if ((currentID.y.Equals(adjacentID.y) && (currentID.x + 1).Equals(adjacentID.x)))
            {
                currentTile.PlayAnimation(TileMovement.Left);
                adjacentTile.PlayAnimation(TileMovement.Right);

            }
            else if ((currentID.y.Equals(adjacentID.y) && (currentID.x - 1).Equals(adjacentID.x)))
            {
                currentTile.PlayAnimation(TileMovement.Right);
                adjacentTile.PlayAnimation(TileMovement.Left);
            }
            else if ((currentID.x.Equals(adjacentID.x) && (currentID.y + 1).Equals(adjacentID.y)))
            {
                currentTile.PlayAnimation(TileMovement.Down);
                adjacentTile.PlayAnimation(TileMovement.Up);
            }
            else if ((currentID.x.Equals(adjacentID.x) && (currentID.y - 1).Equals(adjacentID.y)))
            {
                currentTile.PlayAnimation(TileMovement.Up);
                adjacentTile.PlayAnimation(TileMovement.Down);
            }

            currentTile.OverrideSorting = false;
            currentTile.SortingOrder = 0;
            return true;
        }
        else return false;
    }


}

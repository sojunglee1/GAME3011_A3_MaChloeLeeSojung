using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    public Tile tile;
    public List<Tile> tileList;

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
        print(random);
    }

    public void OnDrop(PointerEventData eventData)
    {


        tile = eventData.pointerDrag.gameObject.GetComponent<Tile>();
        if (eventData.pointerDrag != null)
        {
            if (CanSwitchTiles(eventData))
            {
                SwitchTiles(eventData);
                //eventData.pointerDrag.gameObject.transform.position = transform.position;
                tile.CancelMove();
            }
        }
    }

    public bool CanSwitchTiles(PointerEventData eventData)
    {
        bool move = false;
        if (transform.childCount.Equals(1) && eventData.pointerDrag.gameObject.GetComponent<Tile>().CanMove())
            move = true;
        return move;
    }

    public void SwitchTiles(PointerEventData eventData)
    {
        Tile tile1 = eventData.pointerDrag.GetComponent<Tile>();
        Tile tile2 = transform.GetChild(0).GetComponent<Tile>();

        Transform tile1Parent = eventData.pointerDrag.gameObject.GetComponent<Tile>().transform.parent;
        Transform tile2Parent = this.transform;

        tile1.RemoveParent();
        tile2.RemoveParent();

        tile1.SetParent(tile2Parent);
        tile2.SetParent(tile1Parent);
    }
}

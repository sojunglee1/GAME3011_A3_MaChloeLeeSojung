using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    [Range(4, 8)]
    public int boardWidth, boardHeight;

    public GameObject gameBoard;

    public Slot slot;
    public List<Slot> slotList;

    public Slot[,] slots;

    [System.Obsolete]
    private void Start()
    {
        slots = new Slot[boardWidth, boardHeight];
        CreateGameBoard();
    }

    public void CreateGameBoard()
    {
        float centerX = gameBoard.GetComponent<RectTransform>().rect.width / 2;
        float centerY = gameBoard.GetComponent<RectTransform>().rect.height / 2;

        float itemWidth = slot.GetComponent<RectTransform>().rect.width;
        float itemHeight = slot.GetComponent<RectTransform>().rect.height;

        for (int x =  0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                Vector3 pos = new Vector3((centerX - (x - boardWidth / 2) * itemWidth), (centerY - (y - boardHeight / 2) * itemHeight), 0);
                var newSlot = Instantiate(slot, pos, Quaternion.identity, gameBoard.transform);
                newSlot.ID = new Vector2(x, y);
                newSlot.CreateTile();
                slotList.Add(newSlot.GetComponent<Slot>());
                slots[x, y] = newSlot.GetComponent<Slot>();
            }
        }

        foreach (Slot slot in slotList)
        {
            CheckMatch(slot);
        }
    }

    public void Print()
    {
        print("button press");
    }

    public Slot AdjacentSlot(Slot currentSlot, int x, int y)
    {
        x = (int)Mathf.Clamp(x, 0, currentSlot.ID.x);
        y = (int)Mathf.Clamp(y, 0, currentSlot.ID.y);

        return slots[(int)currentSlot.ID.x + x, (int)currentSlot.ID.y + y];
    }


    public void CheckMatch(Slot currentSlot)
    {
        RaycastHit2D hit = Physics2D.Raycast(currentSlot.transform.position, new Vector2(1,0));
        Debug.DrawLine(currentSlot.transform.position, new Vector2(1, 0));

        if (hit.collider != null)
        {
            //if (hit.collider.gameObject.GetComponent<Slot>().tile.type.Equals(currentSlot.tile.type))
            //{
            //    hit.collider.gameObject.GetComponent<Slot>().tile.Fade();
            //    print(hit.collider.gameObject);
            //}
        }


        int counter = 0;
        for(int x = 0; x < boardWidth; x++)
        {
            for(int y = 0; y < boardHeight; y++)
            {
                //var bottom = AdjacentSlot(currentSlot, -1, 0);
                //var top = AdjacentSlot(currentSlot, 1, 0);
                //var left = AdjacentSlot(currentSlot, 0, -1);
                //var right = AdjacentSlot(currentSlot, 0, 1);


            }
        }

        print(counter);

        //int tileMatch = 0;
        //for (int i = 0; i < slotList.Count - 1; i++)
        //{
        //    if (slotList[i].tile.type.Equals(slotList[i + 1].tile.type))
        //    {
        //        tileMatch++;
        //        print(slotList[i].tile.type + " + " + slotList[i + 1].tile.type);
        //        slotList[i].tile.Fade();
        //        slotList[i+1].tile.Fade();
        //    }
        //    else tileMatch = 0;

        //    if (tileMatch > 2)
        //    {
        //        print("match!");
        //    }
        //}

        //Tile bottom = null, top = null, right = null, left = null;
        //foreach (Slot slot in slotList)
        //{
        //    if (slot.ID.Equals(new Vector2(currentSlot.ID.x, currentSlot.ID.y - 1)) &&
        //        slot.tile.type.Equals(currentSlot.tile.type))
        //    {
        //        bottom = slot.tile;
        //    }
        //    if (slot.ID.Equals(new Vector2(currentSlot.ID.x, currentSlot.ID.y + 1)) &&
        //        slot.tile.type.Equals(currentSlot.tile.type))
        //    {
        //        top = slot.tile;
        //    }

        //    if (slot.ID.Equals(new Vector2(currentSlot.ID.x + 1, currentSlot.ID.y)) &&
        //        slot.tile.type.Equals(currentSlot.tile.type))
        //    {
        //        right = slot.tile;
        //    }
        //    if (slot.ID.Equals(new Vector2(currentSlot.ID.x - 1, currentSlot.ID.y)) &&
        //        slot.tile.type.Equals(currentSlot.tile.type))
        //    {
        //        left = slot.tile;
        //    }
        //}
        //if (bottom != null & top != null)
        //{
        //    bottom.Fade();
        //    top.Fade();
        //    currentSlot.tile.Fade();
        //}

        //if (right != null && left != null)
        //{
        //    right.Fade();
        //    left.Fade();
        //    currentSlot.tile.Fade();
        //}

    }
}

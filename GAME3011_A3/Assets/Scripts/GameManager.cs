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

    private bool GameBoardCreated = false;

    [System.Obsolete]
    private void Start()
    {
        CreateGameBoard();
    }

    public void CreateGameBoard()
    {
        float centerX = gameBoard.GetComponent<RectTransform>().rect.width / 2;
        float centerY = gameBoard.GetComponent<RectTransform>().rect.height / 2;

        float itemWidth = slot.GetComponent<RectTransform>().rect.width;
        float itemHeight = slot.GetComponent<RectTransform>().rect.height;

        for (float x =  0; x < boardWidth; x++)
        {
            for (int y = 0; y < boardHeight; y++)
            {
                Vector3 pos = new Vector3((centerX - (x - boardWidth / 2) * itemWidth), (centerY - (y - boardHeight / 2) * itemHeight), 0);
                var newSlot = Instantiate(slot, pos, Quaternion.identity, gameBoard.transform);
                newSlot.ID = new Vector2(x, y);
                newSlot.CreateTile();
                slotList.Add(newSlot.GetComponent<Slot>());
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


    public void CheckMatch(Slot currentSlot)
    {
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

        Tile bottom = null, top = null, right = null, left = null;
        foreach (Slot slot in slotList)
        {
            if (slot.ID.Equals(new Vector2(currentSlot.ID.x, currentSlot.ID.y - 1)) &&
                slot.tile.type.Equals(currentSlot.tile.type))
            {
                bottom = slot.tile;
            }
            if (slot.ID.Equals(new Vector2(currentSlot.ID.x, currentSlot.ID.y + 1)) &&
                slot.tile.type.Equals(currentSlot.tile.type))
            {
                top = slot.tile;
            }

            if (slot.ID.Equals(new Vector2(currentSlot.ID.x + 1, currentSlot.ID.y)) &&
                slot.tile.type.Equals(currentSlot.tile.type))
            {
                right = slot.tile;
            }
            if (slot.ID.Equals(new Vector2(currentSlot.ID.x - 1, currentSlot.ID.y)) &&
                slot.tile.type.Equals(currentSlot.tile.type))
            {
                left = slot.tile;
            }
        }
        if (bottom != null & top != null)
        {
            bottom.Fade();
            top.Fade();
            currentSlot.tile.Fade();
        }

        if (right != null && left != null)
        {
            right.Fade();
            left.Fade();
            currentSlot.tile.Fade();
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Range(8, 16)]
    public int boardWidth, boardHeight;

    public GameObject gameBoard;

    public Slot slot;
    public List<Slot> slotList;

    //public GameObject tile;
    //public List<Tile> tileList;

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
                newSlot.SetID(new Vector2(x, y));
                slotList.Add(newSlot.GetComponent<Slot>());
            }
        }
    }
}

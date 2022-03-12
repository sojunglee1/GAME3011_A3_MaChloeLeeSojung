using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Range(8, 16)]
    public int boardWidth, boardHeight;

    public GameObject gameBoard;

    public GameObject slot;
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

        for (float i = -boardWidth / 2; i < boardWidth / 2; i++)
        {
            for (int j = -boardHeight / 2; j < boardHeight / 2; j++)
            {
                Vector3 pos = new Vector3(centerX - (i * itemWidth), centerY - (j * itemHeight), 0);
                var newSlot = Instantiate(slot, pos, Quaternion.identity, gameBoard.transform);

                slotList.Add(newSlot.GetComponent<Slot>());
            }
        }
    }
}

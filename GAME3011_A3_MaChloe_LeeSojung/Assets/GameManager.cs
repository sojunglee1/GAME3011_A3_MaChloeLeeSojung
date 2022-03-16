using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Range(8, 16)]
    public int boardWidth;
    [Range(8, 16)]
    public int boardHeight;

    public GameObject tile;
    public GameObject gameBoard;

    public GameObject[,] tiles;

    void Start()
    {
        tiles = new GameObject[boardWidth + 1, boardHeight + 1];
        float boundsX = tile.GetComponent<BoxCollider2D>().size.x;
        for (int x = 0; x <= boardWidth; x++)
        {
            for (int y = 0; y <= boardHeight; y++)
            {
                var newTile = Instantiate(tile, new Vector3(x - (float)boardWidth/2, y - (float)boardHeight/2, 0), Quaternion.identity, gameBoard.transform);
                tiles[x, y] = newTile;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

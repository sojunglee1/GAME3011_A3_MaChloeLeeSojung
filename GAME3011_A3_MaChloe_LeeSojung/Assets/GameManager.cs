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

    private float tileX;
    private Bounds bounds;

    void Start()
    {
        tileX = tile.GetComponent<BoxCollider2D>().offset.x;
        float boundsX = tile.GetComponent<BoxCollider2D>().size.x;
        for (int x = 0; x <= boardWidth; x++)
        {
            for (int y = 0; y <= boardHeight; y++)
            {
                Instantiate(tile, new Vector3(x - (float)boardWidth/2, y - (float)boardHeight/2, 0), Quaternion.identity, gameBoard.transform);
            }

        }

        print(boardWidth / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

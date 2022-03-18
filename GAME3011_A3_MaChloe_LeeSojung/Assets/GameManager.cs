using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DifficultyLevel
{
    Easy = 3,
    Medium = 4,
    Hard = 5 
}

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    [Range(2, 16)]
    public int boardWidth;
    [Range(2, 16)]
    public int boardHeight;

    public Tile tile;
    public GameObject gameBoard;
    public Dictionary<Vector2, Tile> tiles;

    public DifficultyLevel level;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        tiles = new Dictionary<Vector2, Tile>(boardWidth * boardHeight);

        for (int x = 0; x <= boardWidth; x++)
        {
            for (int y = 0; y <= boardHeight; y++)
            {
                var newTile = Instantiate(tile, new Vector3(x - (float)boardWidth/2, y - (float)boardHeight/2, 0), Quaternion.identity, gameBoard.transform);
                newTile.DrawTile();
                newTile.ID = new Vector2(newTile.transform.position.x, newTile.transform.position.y);
                tiles[newTile.ID] = newTile;
            }
        }
    }
}

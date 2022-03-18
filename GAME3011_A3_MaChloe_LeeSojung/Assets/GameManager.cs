using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [Range(2, 16)]
    public int boardWidth;
    [Range(2, 16)]
    public int boardHeight;

    public Tile tile;
    public GameObject gameBoard;
    public Dictionary<Vector2, Tile> tiles;

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

    public void SwitchTiles()
    {
        List<Tile> selectedTiles = new List<Tile>(2);
        for(int x = -boardHeight / 2; x <= boardHeight / 2; x++)
        {
            for (int y = -boardWidth / 2; y <= boardWidth / 2; y++)
            {
                if (tiles[new Vector2(x, y)].isSelected && selectedTiles.Count < 2)
                {
                    selectedTiles.Add(tiles[new Vector2(x, y)]);
                }

                if (selectedTiles.Count > 2)
                {
                    selectedTiles.Clear();
                    foreach (Tile tile in selectedTiles)
                    {
                        tile.isSelected = false;
                    }
                    return;
                }
            }
        }

        if (selectedTiles.Count.Equals(2))
        {
            var tile1 = selectedTiles[0];
            var tile2 = selectedTiles[1];

            if (tile1.isSelected && tile2.isSelected)
            {
                var tile1Pos = tile1.transform.position;
                var tile2Pos = tile2.transform.position;

                tile1.transform.position = tile2Pos;
                tile2.transform.position = tile1Pos;

                tile1.isSelected = false;
                tile2.isSelected = false;

                tile1.ID = new Vector2(tile1.transform.position.x, tile1.transform.position.y);
                tile2.ID = new Vector2(tile2.transform.position.x, tile2.transform.position.y);
            }
        }
    }
}

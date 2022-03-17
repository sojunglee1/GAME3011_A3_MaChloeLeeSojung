using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void FindMatch()
    {

        var hitData = Physics2D.Raycast(tiles[new Vector2(0,0)].topRay.origin, tiles[Vector2.zero].topRay.direction, 2.0f);
        var hitDataCollider = hitData.collider;
        if (hitData)
        {
            print(hitData.transform.position);
            if (tiles[Vector2.zero].sprite.Equals(hitDataCollider.GetComponent<Tile>().sprite))
            {
                print("match!");
                print(hitData.collider.gameObject.GetComponent<Tile>().ID);
            }
            else
            {
                print("no match!");
            }
        }
        else print("didn't hit!");
    }

}

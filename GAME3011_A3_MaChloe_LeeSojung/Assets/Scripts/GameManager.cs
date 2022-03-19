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

public enum GameStatus
{
    Started,
    Ended,
    None
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

    public int score;
    public float timeLeft;

    public GameStatus status;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        tiles = new Dictionary<Vector2, Tile>(boardWidth * boardHeight);
        status = GameStatus.None;
    }

    private void Update()
    {
        if ((int)timeLeft <= 0)
        {
            gameBoard.SetActive(false);
            status = GameStatus.Ended;
            
            return;
        }

        if (status.Equals(GameStatus.Started))
        {
            Countdown();
        }
    }

    public void ChangeDifficulty(string lvl)
    {
        switch (lvl)
        {
            case "Easy": level = DifficultyLevel.Easy; break;
            case "Medium": level = DifficultyLevel.Medium; break;
            case "Hard": level = DifficultyLevel.Hard; break;
        }
    }

    public void StartGame()
    {
        status = GameStatus.Started;
        for (int x = 0; x <= boardWidth; x++)
        {
            for (int y = 0; y <= boardHeight; y++)
            {
                var newTile = Instantiate(tile, new Vector3(x - (float)boardWidth / 2, y - (float)boardHeight / 2, 0), Quaternion.identity, gameBoard.transform);
                newTile.DrawTile();
                newTile.ID = new Vector2(newTile.transform.position.x, newTile.transform.position.y);
                tiles[newTile.ID] = newTile;
            }
        }
    }

    public void Countdown()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
    }
}

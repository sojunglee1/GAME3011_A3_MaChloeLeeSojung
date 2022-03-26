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
    public Candy candyType;
    public static int powerUpCount;
    public static int totalPowerUp;
    public static int XPos;
    public static int YPos;
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
            case "Easy": level = DifficultyLevel.Easy;
                totalPowerUp = (int)((boardHeight * boardWidth) * 0.25);
                break;
            case "Medium": level = DifficultyLevel.Medium;
                totalPowerUp = (int)((boardHeight * boardWidth) * 0.15);
                break;
            case "Hard": level = DifficultyLevel.Hard;
                totalPowerUp = (int)((boardHeight * boardWidth) * 0.1);
                break;
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
                XPos = Random.Range(-4, 4);
                YPos = Random.Range(-4, 4);
                tiles[newTile.ID] = newTile;
            }
        }

        gameBoard.GetComponent<Animator>().Play("Start Animation");
    }

    public void Countdown()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
    }
}

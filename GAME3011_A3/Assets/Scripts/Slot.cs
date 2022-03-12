using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Tile tile;
    public List<Tile> tileList;

    private void Awake()
    {
        Random.seed = Random.Range(0, 100);
    }

    private void Start()
    {
        CreateTile();
    }

    public void CreateTile()
    {
        int random = Random.Range(0, tileList.Count);
        Tile newItem = Instantiate(tileList[random], transform.position, Quaternion.identity, transform);
        print(random);
    }
}

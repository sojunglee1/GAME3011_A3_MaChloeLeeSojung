using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Candy{
    Normal,
    Powerup
}
public class Tile : MonoBehaviour, IPointerDownHandler
{
    public List<Sprite> possibleTiles;
    public Vector2 ID { get; set; }

    public List<Ray2D> allRays;

    public List<RaycastHit2D> rays;
    public bool isSelected { get; set; }

    private static Tile previousSelected = null;

    public List<Tile> selectedTiles;
    [SerializeField] private AudioSource audio;
    public Candy type;
    public Sprite sprite
    {
        get { return GetComponent<SpriteRenderer>().sprite; }
        set { GetComponent<SpriteRenderer>().sprite = value; }
    }

    private void Start()
    {

        audio = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();
        allRays = new List<Ray2D> 
        {
            new Ray2D(transform.position, Vector3.up), 
            new Ray2D(transform.position, Vector3.down), 
            new Ray2D(transform.position, Vector3.left), 
            new Ray2D(transform.position, Vector3.right), 
        };
    }

    public void DrawTile()
    {
        type = Random.value < .5 ? Candy.Normal : Candy.Powerup;
        if (GameManager.powerUpCount >= GameManager.totalPowerUp)
        {
            type = Candy.Normal;
        }
           
        //Debug.Log(string.Format("{0}={1}", Random.value, type));
        //sprite = possibleTiles[Random.Range(0, possibleTiles.Count)];
        switch (type)
        {
            case Candy.Powerup:
                if (GameManager.powerUpCount <= GameManager.totalPowerUp)
                {
                    sprite = possibleTiles[6];
                    GameManager.powerUpCount++;
                }
                break;
            case Candy.Normal:
                sprite = possibleTiles[Random.Range(0, 5)];
                break;
        }


    }

    public void OnDrawGizmos()
    {
        foreach(Ray2D ray in allRays)
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
        }
    }

    public void Selected()
    {
        isSelected = true;
        selectedTiles.Add(this);
        previousSelected = gameObject.GetComponent<Tile>();
        this.PlayAnimation("Fade");
    }

    public void DeSelected()
    {
        isSelected = false;
        selectedTiles.Remove(this);
        previousSelected = null;
        this.PlayAnimation("Default");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isSelected)
        { // Is it already selected?
            DeSelected();
        }
        else
        {
            if (previousSelected == null)
            { // Is it the first tile selected?
                Selected();
            }
            else
            {
                if (GetAllAdjacentTiles().Contains(previousSelected.gameObject))
                {
                    SwitchTiles(previousSelected);
                    previousSelected.ClearAllMatches();
                    previousSelected.DeSelected();
                    ClearAllMatches();
                }
                else
                {
                    previousSelected.GetComponent<Tile>().DeSelected();
                    Selected();
                }
            }
        }
    }

    public void SwitchTiles(Tile previousTile)
    {
        var previousSprite = previousTile.sprite;
        previousTile.sprite = this.sprite;
        this.sprite = previousSprite;
    }

    private GameObject GetAdjacent(Ray2D castDir)
    {
        var hit = Physics2D.Raycast(transform.position, castDir.direction);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    private List<GameObject> GetAllAdjacentTiles()
    {
        List<GameObject> adjacentTiles = new List<GameObject>();
        for (int i = 0; i < allRays.Count; i++)
        {
            adjacentTiles.Add(GetAdjacent(allRays[i]));
        }
        return adjacentTiles;
    }

    private List<GameObject> FindMatch(Ray2D ray)
    {
        List<GameObject> matchingTiles = new List<GameObject>();
        var hitData = Physics2D.Raycast(transform.position, ray.direction);
        while (hitData.collider != null && hitData.collider.GetComponent<SpriteRenderer>().sprite == sprite)
        {

            matchingTiles.Add(hitData.collider.gameObject);
            hitData = Physics2D.Raycast(hitData.collider.transform.position, ray.direction);
        }
        return matchingTiles;
    }

    public List<Tile> emptyTiles;
    private void ClearMatch(List<Ray2D> rays)
    {
        Tile localTile;
        List<GameObject> matchingTiles = new List<GameObject>();
        for (int i = 0; i < rays.Count; i++) { matchingTiles.AddRange(FindMatch(rays[i])); }
        if (matchingTiles.Count >= (int)GameManager.inst.level - 1)
        {
            for (int i = 0; i < matchingTiles.Count; i++)
            {
                localTile = matchingTiles[i].GetComponent<Tile>();
                  matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
                emptyTiles.Add(matchingTiles[i].GetComponent<Tile>());
                
                GameManager.inst.score += 50;
                if(localTile.type == Candy.Powerup)
                {
                    GameManager.inst.score += 100;
                    audio.Play();
                }
            }
            matchFound = true;
            Debug.Log(matchFound);
            
        }
        matchingTiles.Clear();
    }

    private bool matchFound = false;
    public void ClearAllMatches()
    {
        if (sprite == null)
            return;
     
        ClearMatch(new List<Ray2D> { allRays[0], allRays[1] });
        ClearMatch(new List<Ray2D> { allRays[2], allRays[3] });

        if (matchFound)
        {
            sprite = null;
            matchFound = false;
            StartCoroutine(UpdateTiles());
            StopCoroutine(UpdateTiles());
        }
        
    }

    public void PlayAnimation(string animationName)
    {
        if (this.gameObject.activeInHierarchy != false)
        {
            GetComponent<Animator>().Play(animationName);
        }
    }

    IEnumerator UpdateTiles()
    {
        yield return new WaitForSeconds(0.5f);

       
            foreach (Tile tile in emptyTiles)
        {
            //Debug.Log(tile.type);
            if (tile.type == Candy.Powerup)
            {
                if(GameManager.powerUpCount > 0)
                GameManager.powerUpCount--;
                //Debug.Log(GameManager.powerUpCount);
            }
            if (tile.sprite == null)
            {
                DrawTile();
                tile.DrawTile();
            }
        }
        emptyTiles.Clear();
    }
}

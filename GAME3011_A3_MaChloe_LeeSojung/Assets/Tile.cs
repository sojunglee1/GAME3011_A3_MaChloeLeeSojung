using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    public List<Sprite> possibleTiles;
    public Vector2 ID { get; set; }

    public Ray2D topRay { get; set; }
    public Ray2D bottomRay { get; set; }
    public Ray2D leftRay { get; set; }
    public Ray2D rightRay { get; set; }

    public List<Ray2D> allRays;

    public List<RaycastHit2D> rays;
    public bool isSelected { get; set; }

    private static Tile previousSelected = null;

    public List<Tile> selectedTiles;

    public Sprite sprite
    {
        get { return GetComponent<SpriteRenderer>().sprite; }
        set { GetComponent<SpriteRenderer>().sprite = value; }
    }

    private void Start()
    { 
        topRay = new Ray2D(transform.position, Vector3.up);
        bottomRay = new Ray2D(transform.position, -Vector3.up);
        leftRay = new Ray2D(transform.position, -Vector3.right);
        rightRay = new Ray2D(transform.position, Vector3.right);

        allRays = new List<Ray2D> { topRay, bottomRay, leftRay, rightRay };
    }

    public void Matched()
    {
        CheckAdjacentTiles(leftRay);
        CheckAdjacentTiles(bottomRay);
        CheckAdjacentTiles(topRay);
        CheckAdjacentTiles(rightRay);
    }

    public List<GameObject> matched = new List<GameObject>();
    public void CheckAdjacentTiles(Ray2D ray)
    {
        var hitData = Physics2D.Raycast(ray.origin, ray.direction, 1.0f);
        if (hitData)
        {
            if (hitData.collider.gameObject.GetComponent<Tile>().sprite.Equals(this.sprite) && !matched.Contains(hitData.collider.gameObject))
            {
                matched.Add(hitData.collider.gameObject);
            }
        }

        if (matched.Count >= 2)
        {
            foreach (GameObject obj in matched)
            {
                print(obj.GetComponent<Tile>().ID);
                obj.SetActive(false);
                Destroy(obj);
                matched.Remove(obj);
            }
            matched.Clear();
        }

        //List<GameObject> matched = new List<GameObject>();
        //var hitData = Physics2D.Raycast(ray.origin, ray.direction, 1.0f);
        //var hitData2 = Physics2D.Raycast(ray.origin, -ray.direction, 1.0f);
        //if (hitData && hitData2)
        //{
        //    if (hitData.collider.gameObject.GetComponent<Tile>().sprite.Equals(this.sprite) &&
        //        hitData2.collider.gameObject.GetComponent<Tile>().sprite.Equals(this.sprite))
        //    {
        //        matched.Add(gameObject);
        //        matched.Add(hitData.collider.gameObject);
        //        matched.Add(hitData2.collider.gameObject);
        //    }
        //}

        //if (matched.Count > 2)
        //{
        //    foreach (GameObject obj in matched)
        //    {
        //        print(obj.GetComponent<Tile>().ID);
        //        obj.SetActive(false);
        //        Destroy(obj);
        //    }
        //    matched.Clear();
        //}
    }

    public void DrawTile()
    {
        sprite = possibleTiles[Random.Range(0, possibleTiles.Count)];
    }

    public void OnDrawGizmos()
    {
        Debug.DrawRay(topRay.origin, topRay.direction, Color.green);
        Debug.DrawRay(bottomRay.origin, bottomRay.direction, Color.green);
        Debug.DrawRay(leftRay.origin, leftRay.direction, Color.green);
        Debug.DrawRay(rightRay.origin, rightRay.direction, Color.green);
    }

    public void Selected()
    {
        isSelected = true;
        selectedTiles.Add(this);
        previousSelected = gameObject.GetComponent<Tile>();
        this.PlayAnimation("Selected");
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
                    previousSelected.DeSelected();
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


    public void PlayAnimation(string animationName)
    {
        if (this.gameObject.activeInHierarchy != false)
        {
            GetComponent<Animator>().Play(animationName);
        }
    }

    public void MoveTilesDown()
    {
        var hitData = Physics2D.Raycast(bottomRay.origin, bottomRay.direction, 1.0f);

        if (!hitData && transform.position.y != -4)
        {
            transform.position -= Vector3.up;
        }
    }
}

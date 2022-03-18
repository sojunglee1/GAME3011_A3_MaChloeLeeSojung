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

    public List<RaycastHit2D> rays;
    public bool Selected { get; set; }

    public Sprite sprite
    {
        get { return GetComponent<SpriteRenderer>().sprite; }
        set { GetComponent<SpriteRenderer>().sprite = value; }
    }

    private void Update()
    { 
        topRay = new Ray2D(transform.position, Vector3.up);
        bottomRay = new Ray2D(transform.position, -Vector3.up);
        leftRay = new Ray2D(transform.position, -Vector3.right);
        rightRay = new Ray2D(transform.position, Vector3.right);

        if (Selected)
        {
            PlayAnimation("Selected");
        }
        else
        {
            PlayAnimation("Default");
        }

        Matched();
        MoveTilesDown();
    }

    public void Matched()
    {
        //CheckAdjacentTiles(leftRay);
        //CheckAdjacentTiles(bottomRay);
        CheckAdjacentTiles(topRay);
        CheckAdjacentTiles(rightRay);
    }

    public void CheckAdjacentTiles(Ray2D ray)
    {
        List<GameObject> matched = new List<GameObject>();
        var hitData = Physics2D.Raycast(ray.origin, ray.direction, 1.0f);
        var hitData2 = Physics2D.Raycast(ray.origin, -ray.direction, 1.0f);
        if (hitData && hitData2)
        {
            if (hitData.collider.gameObject.GetComponent<Tile>().sprite.Equals(this.sprite) &&
                hitData2.collider.gameObject.GetComponent<Tile>().sprite.Equals(this.sprite))
            {
                matched.Add(gameObject);
                matched.Add(hitData.collider.gameObject);
                matched.Add(hitData2.collider.gameObject);
            }
        }
        
        if (matched.Count > 2)
        {
            foreach (GameObject obj in matched)
            {
                print(obj.GetComponent<Tile>().ID);
                obj.SetActive(false);
                Destroy(obj);
            }
            matched.Clear();
        }
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

    public void OnPointerDown(PointerEventData eventData)
    {
        Selected = !Selected;
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

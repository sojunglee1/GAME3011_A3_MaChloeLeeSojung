using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> possibleTiles;

    private void Start()
    {
        int random = Random.Range(0, possibleTiles.Count);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = possibleTiles[random];
    }

    public void OnDrawGizmos()
    {
        Ray2D topRay = new Ray2D(transform.position, Vector3.up);
        Ray2D bottomRay = new Ray2D(transform.position, -Vector3.up);
        Ray2D leftRay = new Ray2D(transform.position, -Vector3.right);
        Ray2D rightRay = new Ray2D(transform.position, Vector3.right);


        Debug.DrawRay(topRay.origin, topRay.direction, Color.green);
        Debug.DrawRay(bottomRay.origin, bottomRay.direction, Color.green);
        Debug.DrawRay(leftRay.origin, leftRay.direction, Color.green);
        Debug.DrawRay(rightRay.origin, rightRay.direction, Color.green);
    }
}

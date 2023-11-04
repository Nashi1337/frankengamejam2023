using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public LayerMask layerMask;

    /// <summary>
    /// Array that holds all possible spawnpoints for Points of Interest (POI)
    /// </summary>
    [SerializeField]
    private GameObject[] _poiSpawns;

    /// <summary>
    /// Array that holds all possible spawnpoints for decoration
    /// </summary>
    [SerializeField]
    private GameObject[] _decorationSpawns;

    [SerializeField]
    private BuyTileManager[] _buyTileManagers;

    [SerializeField]
    private Vector2Int _position;

    private void Start()
    {
        DeleteOverlappingColliders();
    }

    public void SetUp(Vector2Int position)
    {
        _position = position;
        foreach(BuyTileManager buyTileManager in _buyTileManagers)
        {
            buyTileManager.SetUp(_position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            DeleteOverlappingColliders();
        }
    }

    private void DeleteOverlappingColliders()
    {
        foreach (Transform child in transform)
        {
            Collider2D[] colliders = child.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D collider in colliders)
            {
                if (collider.name.Contains("Right"))
                {
                    Collider2D[] collidingColliders = Physics2D.OverlapBoxAll(collider.transform.position, new Vector2(1, 1), 0, layerMask);
                    foreach(Collider2D collider2 in collidingColliders)
                    {
                        if (collider2.name.Contains("Left"))
                        {
                            Destroy(collider.gameObject);
                            Destroy(collider2.gameObject);
                        }
                    }
                }
                else if (collider.name.Contains("Left"))
                {
                    Collider2D[] collidingColliders = Physics2D.OverlapBoxAll(collider.transform.position, new Vector2(1, 1), 0, layerMask);
                    foreach (Collider2D collider2 in collidingColliders)
                    {
                        if (collider2.name.Contains("Right"))
                        {
                            Destroy(collider.gameObject);
                            Destroy(collider2.gameObject);
                        }
                    }
                }
                else if (collider.name.Contains("Up"))
                {
                    Collider2D[] collidingColliders = Physics2D.OverlapBoxAll(collider.transform.position, new Vector2(1, 1), 0, layerMask);
                    foreach (Collider2D collider2 in collidingColliders)
                    {
                        if (collider2.name.Contains("Down"))
                        {
                            Destroy(collider.gameObject);
                            Destroy(collider2.gameObject);
                        }
                    }
                }
                else if (collider.name.Contains("Down"))
                {
                    Collider2D[] collidingColliders = Physics2D.OverlapBoxAll(collider.transform.position, new Vector2(1, 1), 0, layerMask);
                    foreach (Collider2D collider2 in collidingColliders)
                    {
                        if (collider2.name.Contains("Up"))
                        {
                            Destroy(collider.gameObject);
                            Destroy(collider2.gameObject);
                        }
                    }
                }
            }
        }
    }
}

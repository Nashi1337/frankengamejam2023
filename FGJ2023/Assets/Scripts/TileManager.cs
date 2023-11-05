using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public LayerMask layerMask;

    [SerializeField]
    private GameObject[] _decorations;
    [SerializeField]
    private AnimationCurve _decorationSpawnRate;

    [SerializeField]
    private GameObject _poiObject;

    /// <summary>
    /// Array that holds all possible spawnpoints for Points of Interest (POI)
    /// </summary>
    [SerializeField]
    private List<GameObject> _poiSpawns = new List<GameObject>();

    /// <summary>
    /// Array that holds cardinal direction buy managers (left, right, up down)
    /// </summary>
    [SerializeField]
    private BuyTileManager[] _buyTileManagers;

    /// <summary>
    /// Position of the tile
    /// </summary>
    [SerializeField]
    private Vector2Int _position;

    /// <summary>
    /// On instantiating, we remobe colliders to adjacent tiles.
    /// </summary>
    private void Start()
    {
        DeleteOverlappingColliders();
        SpawnPoi();
        Decorate();
    }

    private void SpawnPoi()
    {
        if(_poiObject == null)
        {
            return;
        }
        int randomPoint = Random.Range(0, _poiSpawns.Count);
        Instantiate(_poiObject, _poiSpawns[randomPoint].transform.position, Quaternion.identity, transform);
        _poiSpawns.RemoveAt(randomPoint);
    }

    private void Decorate()
    {
        if(_decorations.Length == 0)
        {
            return;
        }

        List<GameObject> spawnList = new List<GameObject>();
        foreach(GameObject spawnPoint in _poiSpawns)
        {
            Transform[] t = spawnPoint.GetComponentsInChildren<Transform>();
            spawnList.AddRange(t.Select(transform => transform.gameObject));
        }
        for(int i = 0; i < spawnList.Count; i++)
        {
            int randomPoint = Random.Range(0, spawnList.Count);
            int randomDecoration = Random.Range(0, _decorations.Length);

            float nextDecorationChance = _decorationSpawnRate.Evaluate((float)i);
            if (Random.value > nextDecorationChance)
            {
                break;
            }

            Instantiate(_decorations[randomDecoration], spawnList[randomPoint].transform.position, Quaternion.identity, transform);
            spawnList.RemoveAt(randomPoint);
        }
    }

    /// <summary>
    /// Tile and the buy managers need to now their position so they can buy the next tiles in each direction.
    /// </summary>
    /// <param name="position"></param>
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
        if (collision.CompareTag("Player"))
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

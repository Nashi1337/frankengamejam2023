using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the singleton for spawning Tiles. It is self contained and there is a Prefab
/// in the Prefabs folder that you can drag into a scene and it will handling spawning tiles.
/// </summary>
public class TileSpawningManager : MonoBehaviour
{
    /// <summary>
    /// Start tiles are spawned first in order. When there are no more start tiles left,
    /// the list of weigthed tiles will be used. Leave empty for full randomization.
    /// </summary>
    [SerializeField]
    private List<GameObject> _startTiles = new List<GameObject>();

    /// <summary>
    /// List of tiles and their weights. For each unit of weight, the tile is added to a pot
    /// from where there is exactly one tile drawn.
    /// </summary>
    /// <example>
    /// BerryTile with weight 1 and GrassTile with weight 1 makes them equally probable.
    /// BerryTile with weight 2 and GrassTile with weight 1 will make BerryTiles twice as probable.
    /// </example>
    [SerializeField]
    private List<WeightedTile> _weightedTiles = new List<WeightedTile>();

    [SerializeField]
    private List<GameObject> _history = new List<GameObject>();

    /// <summary>
    /// How far the Tiles will be spawned away. A Tile at [1, 0] will be spawned in WorldPosition [1 * Spacing.X, 0 * Spacing.Y]
    /// </summary>
    [SerializeField]
    private Vector2 _spacing = new Vector2(20f, 20f);

    /// <summary>
    /// Singleton instance of the TileSpawningManager so it might be accessed by other scripts.
    /// </summary>
    private static TileSpawningManager _instance = null;

    /// <summary>
    /// Dictionary of the placed tiles.
    /// </summary>
    private Dictionary<Vector2Int, GameObject> _map = new Dictionary<Vector2Int, GameObject>();

    private void Awake()
    {
        SetupSingleton();
    }

    /// <summary>
    /// Destroys this TileSpawningManger if there is already one registered.
    /// If not, this TileSpawningManger becomes the globally available instance.
    /// </summary>
    private void SetupSingleton()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Deletes history from previous plays/editor tests and spawns the basetile.
    /// </summary>
    private void Start()
    {
        _history.Clear();
        SpawnTile(Vector2Int.zero);
    }

    /// <summary>
    /// Spawns a tile at a given position. When the start tiles are used up,
    /// randomly generated tiles with weights will be used. If there is already a tile
    /// at the position, the generation is rejected.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool SpawnTile(Vector2Int position)
    {
        if(_map.ContainsKey(position))
        {
            return false;
        }

        GameObject temporaryTile = null;
        if(_startTiles.Count > 0)
        {
            temporaryTile = _startTiles[0];
            _startTiles.RemoveAt(0);
        }
        else
        {
            temporaryTile = GetRandomTile();
        }

        if(temporaryTile == null)
        {
            Debug.LogError("There is no tile to spawn!");
            return false;
        }

        GameObject instantiatedTile = Instantiate(
            temporaryTile,
            new Vector3(position.x * _spacing.x, position.y * _spacing.y, 0f),
            Quaternion.identity,
            transform
            );
        instantiatedTile.name = $"{temporaryTile.name}-{position}";
        _history.Add(temporaryTile);
        _map.Add(position, temporaryTile);

        return true;
    }

    /// <summary>
    /// Returns a random tile after considering their weights.
    /// </summary>
    /// <returns></returns>
    private GameObject GetRandomTile()
    {
        List<GameObject> tiles = new List<GameObject>();

        foreach(WeightedTile weightedTile in _weightedTiles)
        {
            for(int i = 0; i < weightedTile.Weight; i++)
            {
                tiles.Add(weightedTile.Tile);
            }
        }

        int randomTile = Random.Range(0, tiles.Count);
        return tiles[randomTile];
    }
}

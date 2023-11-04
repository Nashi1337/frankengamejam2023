using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data class for attaching weights to tiles.
/// </summary>
[System.Serializable]
public class WeightedTile
{
    public GameObject Tile;
    public int Weight = 1;
}

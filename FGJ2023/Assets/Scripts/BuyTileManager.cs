using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public enum TileDirection
{
    Left,
    Up,
    Right,
    Down,
}

public class BuyTileManager : MonoBehaviour, IInteractable
{
    [SerializeField]
    private TileDirection _tileDirection;

    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    private Vector2Int _position;

    public void Interact()
    {
        int cost = 1;
        TileSpawningManager instance = TileSpawningManager.Instance;
        if(_inventory.TileTokenAmount >= cost)
        {
            Vector2Int direction = Vector2Int.zero;
            switch(_tileDirection)
            {
                case TileDirection.Left:
                    direction = Vector2Int.left;
                    break;
                case TileDirection.Right:
                    direction = Vector2Int.right;
                    break;
                case TileDirection.Down:
                    direction = Vector2Int.down;
                    break;
                case TileDirection.Up:
                    direction = Vector2Int.up;
                    break;
            }
            if(instance.SpawnTile(_position + direction))
            {
                _inventory.TileTokenAmount -= cost;
            }
        }
    }

    internal void SetUp(Vector2Int position)
    {
        _position = position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    [SerializeField]
    private int _berryAmount;
    [SerializeField]
    private int _woodAmount;
    [SerializeField]
    private int _stoneAmount;
    [SerializeField]
    private int _fishAmount;
    [SerializeField]
    private int _tileTokenAmount;

    public delegate void OnValueChangedHandler();
    public OnValueChangedHandler OnValueChanged;

    public int BerryAmount
    {
        get
        {
            return _berryAmount;
        }

        set
        {
            _berryAmount = value;
            OnValueChanged?.Invoke();
        }
    }

    public int WoodAmount
    {
        get
        {
            return _woodAmount;
        }

        set
        {
            _woodAmount = value;
            OnValueChanged?.Invoke();
        }
    }

    public int StoneAmount
    {
        get
        {
            return _stoneAmount;
        }

        set
        {
            _stoneAmount = value;
            OnValueChanged?.Invoke();
        }
    }

    public int FishAmount
    {
        get
        {
            return _fishAmount;
        }

        set
        {
            _fishAmount = value;
            OnValueChanged?.Invoke();
        }
    }
    public int TileTokenAmount
    {
        get
        {
            return _tileTokenAmount;
        }

        set
        {
            _tileTokenAmount = value;
            OnValueChanged?.Invoke();
        }
    }

    public void ResetInventory()
    {
        _berryAmount = 0;
        _woodAmount = 0;
        _stoneAmount = 0;
        _fishAmount = 0;
        _tileTokenAmount = 0;
        OnValueChanged?.Invoke();
    }
}

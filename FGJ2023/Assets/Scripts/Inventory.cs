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

    public delegate void OnValueChangedHandler(int newValue);
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
            OnValueChanged?.Invoke(_berryAmount);
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
            OnValueChanged?.Invoke(_woodAmount);
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
            OnValueChanged?.Invoke(_stoneAmount);
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
            OnValueChanged?.Invoke(_fishAmount);
        }
    }
}

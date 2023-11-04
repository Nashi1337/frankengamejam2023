using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryListener : MonoBehaviour
{
    [SerializeField]
    private Inventory _inventory;

    [SerializeField]
    private TextMeshProUGUI _berryAmount;
    [SerializeField]
    private TextMeshProUGUI _fishAmount;
    [SerializeField]
    private TextMeshProUGUI _woodAmount;
    [SerializeField]
    private TextMeshProUGUI _stoneAmount;

    private void Awake()
    {
        if(_inventory == null)
        {
            return;
        }

        _inventory.OnValueChanged += UpdateUi;
        //@todo: This shouldn't be here
        _inventory.ResetInventory();
    }

    private void OnDisable()
    {
        if(_inventory != null )
        {
            _inventory.OnValueChanged -= UpdateUi;
        }
    }

    private void UpdateUi()
    {
        _berryAmount.text = $"{_inventory.BerryAmount}";
        _fishAmount.text = $"{_inventory.FishAmount}";
        _woodAmount.text = $"{_inventory.WoodAmount}";
        _stoneAmount.text = $"{_inventory.StoneAmount}";
    }
}

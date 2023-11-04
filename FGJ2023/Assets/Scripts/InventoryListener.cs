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
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField]
    private Image berryCounter;
    [SerializeField]
    private Image fishCounter;
    [SerializeField]
    private Image woodCounter;
    [SerializeField]
    private Image stoneCounter;
    [SerializeField]
    private Image berryCounter2;
    [SerializeField]
    private Image fishCounter2;
    [SerializeField]
    private Image woodCounter2;
    [SerializeField]
    private Image stoneCounter2;
    [SerializeField]
    private Sprite one;
    [SerializeField]
    private Sprite two;
    [SerializeField]
    private Sprite three;
    [SerializeField]
    private Sprite four;
    [SerializeField]
    private Sprite five;

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
        UpdateBerries();
        UpdateWood();
        UpdateFish();
        UpdateStone();
    }

    private void UpdateBerries()
    {
        _berryAmount.text = $"{_inventory.BerryAmount}";
        if (_inventory.BerryAmount > 0)
        {
            berryCounter.gameObject.SetActive(true);
            if (_inventory.BerryAmount > 5)
                berryCounter2.gameObject.SetActive(true);
        }
        switch (_inventory.BerryAmount)
        {
            case 0:
                berryCounter.sprite = null;
                break;
            case 1:
                berryCounter.sprite = one;
                break;
            case 2:
                berryCounter.sprite = two;
                break;
            case 3:
                berryCounter.sprite = three;
                break;
            case 4:
                berryCounter.sprite = four;
                break;
            case 5:
                berryCounter.sprite = five;
                break;
            case 6:
                berryCounter.sprite = five;
                berryCounter2.sprite = one;
                break;
            case 7:
                berryCounter.sprite = five;
                berryCounter2.sprite = two;
                break;
            case 8:
                berryCounter.sprite = five;
                berryCounter2.sprite = three;
                break;
            case 9:
                berryCounter.sprite = five;
                berryCounter2.sprite = four;
                break;
            case 10:
                berryCounter.sprite = five;
                berryCounter2.sprite = five;
                break;
            default:
                break;
        }
        berryCounter.SetNativeSize();
        berryCounter2.SetNativeSize();
    }

    private void UpdateWood()
    {
        _woodAmount.text = $"{_inventory.WoodAmount}";
        if (_inventory.WoodAmount > 0)
        {
            woodCounter.gameObject.SetActive(true);
            if (_inventory.WoodAmount > 5)
                woodCounter2.gameObject.SetActive(true);
        }
        switch (_inventory.WoodAmount)
        {
            case 0:
                woodCounter.sprite = null;
                break;
            case 1:
                woodCounter.sprite = one;
                break;
            case 2:
                woodCounter.sprite = two;
                break;
            case 3:
                woodCounter.sprite = three;
                break;
            case 4:
                woodCounter.sprite = four;
                break;
            case 5:
                woodCounter.sprite = five;
                break;
            case 6:
                woodCounter.sprite = five;
                woodCounter2.sprite = one;
                break;
            case 7:
                woodCounter.sprite = five;
                woodCounter2.sprite = two;
                break;
            case 8:
                woodCounter.sprite = five;
                woodCounter2.sprite = three;
                break;
            case 9:
                woodCounter.sprite = five;
                woodCounter2.sprite = four;
                break;
            case 10:
                woodCounter.sprite = five;
                woodCounter2.sprite = five;
                break;
            default:
                break;
        }
        woodCounter.SetNativeSize();
        woodCounter2.SetNativeSize();
    }

    private void UpdateFish()
    {
        _fishAmount.text = $"{_inventory.FishAmount}";
        if (_inventory.FishAmount > 0)
        {
            fishCounter.gameObject.SetActive(true);
            if (_inventory.FishAmount > 5)
                fishCounter2.gameObject.SetActive(true);
        }
        switch (_inventory.FishAmount)
        {
            case 0:
                fishCounter.sprite = null;
                break;
            case 1:
                fishCounter.sprite = one;
                break;
            case 2:
                fishCounter.sprite = two;
                break;
            case 3:
                fishCounter.sprite = three;
                break;
            case 4:
                fishCounter.sprite = four;
                break;
            case 5:
                fishCounter.sprite = five;
                break;
            case 6:
                fishCounter.sprite = five;
                fishCounter2.sprite = one;
                break;
            case 7:
                fishCounter.sprite = five;
                fishCounter2.sprite = two;
                break;
            case 8:
                fishCounter.sprite = five;
                fishCounter2.sprite = three;
                break;
            case 9:
                fishCounter.sprite = five;
                fishCounter2.sprite = four;
                break;
            case 10:
                fishCounter.sprite = five;
                fishCounter2.sprite = five;
                break;
            default:
                break;
        }
        fishCounter.SetNativeSize();
        fishCounter2.SetNativeSize();
    }

    private void UpdateStone()
    {
        _stoneAmount.text = $"{_inventory.StoneAmount}";
        if (_inventory.StoneAmount > 0)
        {
            stoneCounter.gameObject.SetActive(true);
            if (_inventory.StoneAmount > 5)
                stoneCounter2.gameObject.SetActive(true);
        }
        switch (_inventory.StoneAmount)
        {
            case 0:
                stoneCounter.sprite = null;
                break;
            case 1:
                stoneCounter.sprite = one;
                break;
            case 2:
                stoneCounter.sprite = two;
                break;
            case 3:
                stoneCounter.sprite = three;
                break;
            case 4:
                stoneCounter.sprite = four;
                break;
            case 5:
                stoneCounter.sprite = five;
                break;
            case 6:
                stoneCounter.sprite = five;
                stoneCounter2.sprite = one;
                break;
            case 7:
                stoneCounter.sprite = five;
                stoneCounter2.sprite = two;
                break;
            case 8:
                stoneCounter.sprite = five;
                stoneCounter2.sprite = three;
                break;
            case 9:
                stoneCounter.sprite = five;
                stoneCounter2.sprite = four;
                break;
            case 10:
                stoneCounter.sprite = five;
                stoneCounter2.sprite = five;
                break;
            default:
                break;
        }
        stoneCounter.SetNativeSize();
        stoneCounter2.SetNativeSize();
    }
}

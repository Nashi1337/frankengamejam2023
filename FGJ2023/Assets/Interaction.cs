using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject SpeechBubble;
    [SerializeField]
    Sprite itemSprite;
    [SerializeField]
    Sprite usedSprite;
    [SerializeField]
    private Inventory _inventory;
    [SerializeField]
    bool give;
    [SerializeField]
    bool isWorkstation;
    public bool pickupable;
    public bool hasWorker;
    //0=berry, 1=wood, 2=fish, 3=stone
    [SerializeField]
    public int type;
    public int amount;

    private void Awake()
    {
        amount = Random.Range(1, 5);
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 2; i < amount + 2 && i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = itemSprite;
        }
        SetInteractivity(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetInteractivity(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetInteractivity(false);
        }
    }

    public void Interact()
    {
        //I hate dis, the type should be scriptable objects too.
        if (!isWorkstation)
        {
            switch (type)
            {
                case 0:
                    if (give)
                    {
                        _inventory.BerryAmount += amount;
                        ReplaceSprite();
                    }
                    else
                    {
                        if (_inventory.BerryAmount >= amount)
                        {
                            _inventory.BerryAmount -= amount;
                            PickUpDino();
                        }
                    }
                    break;
                case 1:
                    if (give)
                    {
                        _inventory.WoodAmount += amount;
                        ReplaceSprite();
                    }
                    else
                    {
                        if (_inventory.WoodAmount >= amount)
                        {
                            _inventory.WoodAmount -= amount;
                            Destroy(gameObject);
                        }
                    }
                    break;
                case 2:
                    if (give)
                    {
                        _inventory.FishAmount += amount;
                        ReplaceSprite();
                    }
                    else
                    {
                        if (_inventory.FishAmount >= amount)
                        {
                            _inventory.FishAmount -= amount;
                            Destroy(gameObject);
                        }
                    }
                    break;
                case 3:
                    if (give)
                    {
                        _inventory.StoneAmount += amount;
                        ReplaceSprite();
                    }
                    else
                    {
                        if (_inventory.StoneAmount >= amount)
                        {
                            _inventory.StoneAmount -= amount;
                            Destroy(gameObject);
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        else if (isWorkstation)
        {
            if (!hasWorker)
            {

            }
        }
    }

    public void SetInteractivity(bool value)
    {
        if (SpeechBubble != null)
        {
            SpeechBubble.SetActive(value);
        }
    }

    public void ReplaceSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = usedSprite;
        Destroy(gameObject.GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        SetInteractivity(false);
        Destroy(this);
    }

    public void PickUpDino()
    {
        pickupable = true;
    }
}

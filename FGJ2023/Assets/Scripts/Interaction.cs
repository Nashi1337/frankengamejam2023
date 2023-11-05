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

    //0=berry, 1=wood, 2=fish, 3=stone
    [SerializeField]
    public int type;
    public int amount;

    public bool DinoCanBePlaced => false;

    private void Awake()
    {
        amount = Random.Range(1, 5);
        SpriteRenderer[] spriteRenderers = SpeechBubble.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 1; i < amount + 1 && i < spriteRenderers.Length; i++)
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

    public void Interact(PlayerController player)
    {
        //I hate dis, the type should be scriptable objects too.
        switch (type)
        {
            case 0:
                if(player.Inventory.BerryAmount + amount <= 10)
                {
                    player.Inventory.BerryAmount += amount;
                    ReplaceSprite();
                }
                break;
            case 1:
                if (player.Inventory.WoodAmount + amount <= 10)
                {
                    player.Inventory.WoodAmount += amount;
                    ReplaceSprite();
                }
                break;
            case 2:
                if (player.Inventory.FishAmount + amount <= 10)
                {
                    player.Inventory.FishAmount += amount;
                    ReplaceSprite();
                }
                break;
            case 3:
                if (player.Inventory.StoneAmount + amount <= 10)
                {
                    player.Inventory.StoneAmount += amount;
                    ReplaceSprite();
                }
                break;
            default:
                break;
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

    public void PlaceDino(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}

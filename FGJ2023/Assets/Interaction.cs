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
    Sprite newSprite;

    [SerializeField]
    private Inventory _inventory;

    //0=berry
    [SerializeField]
    public int type;
    public int amount;

    private void Awake()
    {
        amount = Random.Range(1, 5);
        Debug.Log(amount);
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        for(int i = 2; i < amount+2; i++)
        {
            spriteRenderers[i].sprite = newSprite;
        }
        SpeechBubble.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SpeechBubble.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SpeechBubble.SetActive(false);
        }
    }

    public void Interact()
    {
        //I hate dis, the type should be scriptable objects too.
        switch(type)
        {
            case 0:
                _inventory.BerryAmount += amount;
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class House : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject SpeechBubble;

    [SerializeField]
    Sprite[] itemSprite;

    [SerializeField]
    private List<int> _cost = new List<int>();

    [SerializeField]
    private AnimationCurve _costProbability;

    [SerializeField]
    Sprite[] houseUpgrades;

    public bool DinoCanBePlaced => false;

    public void Interact(PlayerController player)
    {
        int berryCost = _cost.Count(c => c == 0);
        int woodCost = _cost.Count(c => c == 1);
        int fishCost = _cost.Count(c => c == 2);
        int stoneCost = _cost.Count(c => c == 3);

        if (player.Inventory.BerryAmount >= berryCost &&
            player.Inventory.WoodAmount >= woodCost &&
            player.Inventory.FishAmount >= fishCost &&
            player.Inventory.StoneAmount >= stoneCost)
        {
            player.Inventory.BerryAmount -= berryCost;
            player.Inventory.WoodAmount -= woodCost;
            player.Inventory.FishAmount -= fishCost;
            player.Inventory.StoneAmount -= stoneCost;
            //TODO: find house upgrade SFX
/*            AudioManager instance = AudioManager.Instance;
            instance.DinoChew();*/
        }
    }

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if (Random.value <= _costProbability.Evaluate((float)i))
            {
                _cost.Add(Random.Range(0, 4));
            }
        }

        SpriteRenderer[] spriteRenderers = SpeechBubble.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 1; i < _cost.Count + 1 && i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].sprite = itemSprite[_cost[i - 1]];
        }
        SetInteractivity(false);
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

    public void SetInteractivity(bool value)
    {
        if (SpeechBubble != null)
        {
            SpeechBubble.SetActive(value);
        }
    }

    public void PlaceDino(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}

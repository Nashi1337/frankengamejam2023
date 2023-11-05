using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dino : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject SpeechBubble;

    [SerializeField]
    Sprite[] itemSprite;

    [SerializeField]
    private List<int> _cost = new List<int>();

    [SerializeField]
    private AnimationCurve _costProbability;

    public int InteractionPriority(PlayerController player)
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
            return 20;
        }

        return 0;
    }

    public bool DinoCanBePlaced => false;

    public void Interact(PlayerController player)
    {
        int berryCost = _cost.Count(c => c == 0);
        int woodCost = _cost.Count(c => c == 1);
        int fishCost = _cost.Count(c => c == 2);
        int stoneCost = _cost.Count(c => c == 3);

        if(player.Inventory.BerryAmount >= berryCost &&
            player.Inventory.WoodAmount >= woodCost &&
            player.Inventory.FishAmount >= fishCost &&
            player.Inventory.StoneAmount >= stoneCost)
        {
            player.Inventory.BerryAmount -= berryCost;
            player.Inventory.WoodAmount -= woodCost;
            player.Inventory.FishAmount -= fishCost;
            player.Inventory.StoneAmount -= stoneCost;
            AudioManager instance = AudioManager.Instance;
            instance.DinoChew();
            player.TakeDino(this);
        }
    }

    public void PlaceDino(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 4; i++)
        {
            if(Random.value <= _costProbability.Evaluate((float)i))
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

    // Update is called once per frame
    void Update()
    {
        
    }
}

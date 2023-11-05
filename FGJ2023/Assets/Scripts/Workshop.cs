using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour, IInteractable
{
    [SerializeField]
    GameObject SpeechBubble;

    [SerializeField]
    private bool _hasDino = false;

    public int InteractionPriority(PlayerController player)
    {
        return 0;
    }

    public bool DinoCanBePlaced => !_hasDino;

    [SerializeField]
    private int _resourceType;

    [SerializeField]
    private float _timeBetweenResource;

    [SerializeField]
    private float _timeToNextResource;

    [SerializeField]
    private Inventory _inventory;

    public void Interact(PlayerController player)
    {
        throw new System.NotImplementedException("Not implemented here");
    }

    public void PlaceDino(PlayerController player)
    {
        _hasDino = true;
        _inventory = player.Inventory;
        _timeToNextResource = _timeBetweenResource;
        GameObject dino = player.HeldDino.gameObject;
        dino.transform.SetParent(transform, false);
        dino.transform.localScale = Vector3.one;
        Destroy(dino.GetComponent<Dino>());
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_hasDino)
        {
            _timeToNextResource -= Time.deltaTime;
            if(_timeToNextResource < 0)
            {
                switch(_resourceType)
                {
                    case 0:
                        _inventory.BerryAmount = Mathf.Min(_inventory.BerryAmount + 1, 10);
                        break;

                    case 1:
                        _inventory.WoodAmount = Mathf.Min(_inventory.WoodAmount + 1, 10);
                        break;

                    case 2:
                        _inventory.FishAmount = Mathf.Min(_inventory.FishAmount + 1, 10);
                        break;

                    case 3:
                        _inventory.StoneAmount = Mathf.Min(_inventory.StoneAmount + 1, 10); ;
                        break;
                }
                _timeToNextResource = _timeBetweenResource;
            }
        }
    }
}

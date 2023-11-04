using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private int differentItems = 5;
    [SerializeField]
    private GameObject berryAmount;

    private Rigidbody2D rigidbody;
    private Vector2 movementInput;
    private Collider2D collider;
    private bool canInteract;
    private GameObject Interactable;
    private int[] inventory;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponentInChildren<Collider2D>();
        inventory = new int[differentItems];
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = movementInput * speed;
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.Space))
        {
            int itemType = Interactable.GetComponent<Interaction>().type;
            int itemAmount = Interactable.GetComponent<Interaction>().amount;
            Debug.Log(itemType + " " + itemAmount);
            inventory[itemType] += itemAmount;

            berryAmount.GetComponent<TextMeshProUGUI>().text = inventory[itemType].ToString();

            Destroy(Interactable);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canInteract = true;
            Interactable = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable"))
        {
            canInteract = false;
            Interactable = null;
        }
    }
}

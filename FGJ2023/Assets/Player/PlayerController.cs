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

    private Rigidbody2D rigidbody;
    private Vector2 movementInput;

    private IInteractable _interactable = null;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = movementInput * speed;
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void OnAction(InputValue inputValue)
    {
        _interactable?.Interact();
    }

    //private void Update()
    //{
    //    if (canInteract && Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Interaction interactable = Interactable.GetComponent<Interaction>();
    //        int itemType = interactable.type;
    //        int itemAmount = interactable.amount;
    //        Debug.Log(itemType + " " + itemAmount);
    //        if (interactable.give)
    //        {
    //            inventory[itemType] += itemAmount;
    //            Destroy(Interactable);
    //        }
    //        else
    //        {
    //            if (inventory[itemType] >= interactable.amount)
    //            {
    //                inventory[itemType] -= interactable.amount;
    //                interactable.ToggleInteractivity();
    //            }
    //            else
    //            {
    //                Debug.Log("Not enough items");
    //            }
    //        }

    //        berryAmount.GetComponent<TextMeshProUGUI>().text = inventory[itemType].ToString();

 
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        _interactable = other.GetComponent<IInteractable>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _interactable = collision.collider.GetComponent<IInteractable>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _interactable = null;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _interactable = null;
    }
}

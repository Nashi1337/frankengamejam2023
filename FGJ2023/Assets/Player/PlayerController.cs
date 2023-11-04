using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 10f;
    [SerializeField]
    private float sprintSpeed = 20f;

    private Rigidbody2D rigidbody;
    private Vector2 movementInput;
    private bool isWalking;

    private IInteractable _interactable = null;
    private Animator animator;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = movementInput * (isWalking ? walkSpeed : sprintSpeed);
        animator.SetFloat("movementX", movementInput.x);
        animator.SetFloat("movementY", movementInput.y);
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

    private void OnWalk(InputValue inputValue)
    {
        isWalking = inputValue.isPressed;
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

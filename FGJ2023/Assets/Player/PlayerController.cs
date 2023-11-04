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

    private IInteractable _interactable;

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
        if(_interactable != null)
        {
            _interactable.Interact();
        }
    }

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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;

    private Rigidbody2D rigidbody;
    private Vector2 movementInput;
    private Collider2D collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponentInChildren<Collider2D>();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = movementInput * speed;
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }

}

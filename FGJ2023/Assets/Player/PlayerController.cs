using JetBrains.Annotations;
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
    private bool lookLeft = false;
    private bool previousLookLeft = false;

    private IInteractable _interactable = null;
    private Animator animator;
    private GameObject DinoHolder;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        DinoHolder = gameObject.transform.Find("DinoHolder").gameObject;
    }

    private void FixedUpdate()
    {
        float speed = (isWalking ? walkSpeed : sprintSpeed);
        rigidbody.velocity = movementInput * speed;
        bool vertical = !Mathf.Approximately(movementInput.y, 0f);
        float animationSpeed = speed / walkSpeed;
        animator.SetFloat("movementX", vertical ? 0f : movementInput.x * animationSpeed);
        animator.SetFloat("movementY", movementInput.y * animationSpeed);
        if (movementInput.x < 0f)
        {
            lookLeft = true;
        }
        else if (movementInput.x > 0f)
        {
            lookLeft = false;
        }
        if (lookLeft != previousLookLeft)
        {
            if (lookLeft)
            {
                DinoHolder.transform.localPosition = new Vector3(-DinoHolder.transform.localPosition.x, DinoHolder.transform.localPosition.y, DinoHolder.transform.localPosition.z);

            }
            else
            {
                DinoHolder.transform.localPosition = new Vector3(Mathf.Abs(DinoHolder.transform.localPosition.x), DinoHolder.transform.localPosition.y, DinoHolder.transform.localPosition.z);

            }
            previousLookLeft = lookLeft;
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if(interactable != null)
        {
            _interactable = interactable;
            if (other.gameObject.tag == "Dino")
            {
                if (other.gameObject.GetComponent<Interaction>().pickupable)
                {
                    other.gameObject.transform.SetParent(DinoHolder.gameObject.transform,false);
                }
            }
        }
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

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 10f;
    [SerializeField]
    private float sprintSpeed = 20f;

    [SerializeField]
    private Inventory _inventory;

    public Inventory Inventory
    {
        get
        {
            return _inventory;
        }
    }

    private Rigidbody2D rigidbody;
    private Vector2 movementInput;
    private bool isWalking;
    private bool lookLeft = false;
    private bool previousLookLeft = false;

    [SerializeField]
    private Dino _heldDino = null;

    public Dino HeldDino
    {
        get
        {
            return _heldDino;
        }
    }

    private HashSet<IInteractable> _interactables = new HashSet<IInteractable>();
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
        _interactables.RemoveWhere(interactable => (interactable as Component).IsDestroyed());
        IInteractable dinoInteractable = _interactables.FirstOrDefault(i => i.DinoCanBePlaced);
        //If we hold a dino and are at a workstation that can have a dino, we place the dino
        if (_heldDino != null && dinoInteractable != null)
        {
            dinoInteractable.PlaceDino(this);
            _heldDino = null;
        }
        // if we're not holding a dino, we just interact with the first thing in the list
        else
        {
            _interactables.FirstOrDefault()?.Interact(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            _interactables.Add(interactable);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IInteractable interactable = collision.collider.GetComponent<IInteractable>();
        if (interactable != null)
        {
            _interactables.Add(interactable);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IInteractable interactable = collision.collider.GetComponent<IInteractable>();
        if (interactable != null)
        {
            _interactables.Remove(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            _interactables.Remove(interactable);
        }
    }

    public bool TakeDino(Dino dino)
    {
        if(_heldDino != null)
        {
            return false;
        }
        _heldDino = dino;
        _heldDino.gameObject.transform.SetParent(DinoHolder.gameObject.transform, false);
        return true;
    }
}

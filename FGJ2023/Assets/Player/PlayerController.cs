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
    private bool holdingDino = false;

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
        //While holding a dino, no interaction with other things possible
        if(holdingDino)
        {
            return;
        }
        _interactables.RemoveWhere(interactable => (interactable as Component).IsDestroyed());
        foreach (IInteractable interactible in _interactables.ToList())
        {
            interactible.Interact(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            _interactables.Add(interactable);
            if (other.gameObject.CompareTag("Dino"))
            {
                if (other.gameObject.GetComponent<Interaction>().pickupable)
                {
                    if (!holdingDino)
                    {
                        other.gameObject.transform.SetParent(DinoHolder.gameObject.transform,false);
                        holdingDino = true;
                    }
                }
            }
            if (other.gameObject.CompareTag("Workstation"))
            {
                if (!other.gameObject.GetComponent<Interaction>().hasWorker)
                {
                    if (holdingDino && DinoHolder.transform.childCount > 0)
                    {
                        GameObject dino = DinoHolder.transform.GetChild(0).gameObject;
                        dino.gameObject.transform.SetParent(other.gameObject.transform,false);
                    }
                }
            }
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
}

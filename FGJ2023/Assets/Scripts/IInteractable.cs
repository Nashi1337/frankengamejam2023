using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerController player);

    bool DinoCanBePlaced
    {
        get;
    }

    void PlaceDino();
}

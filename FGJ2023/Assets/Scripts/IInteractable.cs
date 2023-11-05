using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Interact(PlayerController player);

    int InteractionPriority(PlayerController player);

    bool DinoCanBePlaced
    {
        get;
    }

    void PlaceDino(PlayerController player);
}

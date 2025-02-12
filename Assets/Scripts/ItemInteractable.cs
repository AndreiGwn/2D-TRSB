using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteractable : Interactable
{
    private bool isOpen = false;
    public float doorSpeed = 2f;

    public Animator doorAnimator;

    public override void Interact()
    {
        Debug.Log("Trying to interact with an item");
    }

}
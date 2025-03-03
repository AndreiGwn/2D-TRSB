using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float interactionRange = 3f;
    public Transform playerTransform;
    private Interactable target;


    private void Start()
    {

        foreach (Interactable interactable in Interactable.interactables)
        {
            Debug.Log(interactable);
        }
    }

    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        foreach (Interactable interactable in Interactable.interactables)
        {
            float d = (playerTransform.position - interactable.gameObject.transform.position).sqrMagnitude;
            if (d < interactionRange)
            {
                target = interactable;
            }
        }

        if (target != null && (playerTransform.position - target.gameObject.transform.position).sqrMagnitude > interactionRange)
        {
            target = null;
        }
    }

    private void Interact()
    {
        if (target != null)
        {
            Debug.Log(this.gameObject);
            Debug.Log(target);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public float interactionRange;
    public static readonly HashSet<Interactable> interactables = new HashSet<Interactable>();
    public abstract void Interact();
    public GameObject gameManager;
    public InteractionSystem interactionSystem;

    private void Start()
    {
        Debug.Log(this.gameObject);
        interactables.Add(this);
        if (gameManager == null)
            gameManager = GameObject.Find("GameManager");

        if (interactionSystem == null)
            interactionSystem = gameManager.GetComponent<InteractionSystem>();
    }
    private void OnDestroy()
    {
        interactables.Remove(this);
    }
}
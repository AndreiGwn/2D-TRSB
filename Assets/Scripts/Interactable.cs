using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public static readonly HashSet<Interactable> interactables = new HashSet<Interactable>();
    public abstract void Interact();

    private void Start()
    {
        Debug.Log(this.gameObject);
        interactables.Add(this);
    }

    private void OnDestroy()
    {
        interactables.Remove(this);
    }
}
using UnityEngine;

public class BodyInteractable : Interactable
{

    public override void Interact()
    {
        gameManager.GetComponent<PlayerController>().HotSwap(this.gameObject);
    }
}

using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public CharacterController characterController;
    public InteractionSystem interactionSystem;


    private void Start()
    {
        if (characterController == null)
            characterController = player.GetComponent<CharacterController>();

        if(interactionSystem == null)
            interactionSystem = this.gameObject.GetComponent<InteractionSystem>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            characterController.run = 2;
        }

        else
        {
            characterController.run = 1;
        }

        if (Input.GetKeyDown("space"))
        {
            if (characterController.grounded)
            {
                characterController.Jump();
            }

        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            characterController.crouch = true;
        }

        else
        {
            characterController.crouch = false;
        }

        characterController.move = Input.GetAxisRaw("Horizontal");
    }
    
    //Changes player to new body
    public void HotSwap(GameObject target)
    {
        player = target;
        characterController = target.GetComponent<CharacterController>();
        interactionSystem.playerTransform = target.transform;
        interactionSystem.player = player;

    }
}
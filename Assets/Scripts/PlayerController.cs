using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    private Chassis chassis;

    private Rigidbody2D rb;
    private Collider2D collider;

    private void Start()
    {
        chassis = characterController.chassis;
        rb = characterController.rb;
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

    private void FixedUpdate()
    {

    }
}
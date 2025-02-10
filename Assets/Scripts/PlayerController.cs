using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    private Chassis chassis;
    private int run;
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
            run = 2;
        }

        else
        {
            run = 1;
        }

        if (Input.GetKeyDown("space"))
        {
            if (characterController.grounded)
            {
                characterController.Jump();
            }

        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal") * characterController.chassis.speed * run, rb.linearVelocity.y);

        if (run == 2) 
        {
            characterController.charge =- 1 * Time.deltaTime;
        }


    }
}

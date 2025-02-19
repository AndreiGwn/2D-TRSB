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
        float moveDirection = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveDirection * characterController.chassis.speed * run, rb.linearVelocity.y);

        if (run == 2)
        {
            characterController.charge -= 1 * Time.deltaTime;
        }

        bool isMoving = moveDirection != 0;
        GetComponent<Animator>().SetBool("Moving", isMoving);


        GetComponent<Animator>().SetBool("Sprint", isMoving && run == 2);


        if (moveDirection > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveDirection < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
    }
    }

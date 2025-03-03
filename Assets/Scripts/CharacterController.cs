using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Chassis chassis;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Collider2D collider;
    private Animator animator;
    public bool grounded;

    public float move;
    public int run;
    public bool crouch;

    public float health;
    public float charge;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<Transform>();
        collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocityX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        else if (rb.linearVelocityX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }



        if (move != 0 && (chassis.canCrouchWalk || !crouch))
        {
            rb.linearVelocity = new Vector2(move * chassis.speed * run, rb.linearVelocityY);

            if (run == 2)
            {
                charge = -1 * Time.deltaTime;
            }

            animator.SetBool("Moving", true);

        }

        else if (chassis.canCrouchWalk)
        {
            rb.linearVelocityX = 0;
            animator.SetBool("Moving", false);
        }

        if (crouch)
        {
            animator.SetBool("Crouch", true);
        }

        else
        {
            animator.SetBool("Crouch", false);
        }
    }

    public void Jump()
    {
        rb.AddForce(new Vector2(rb.linearVelocity.x, chassis.jumpHeight * 100));
        Debug.Log("gaming");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}
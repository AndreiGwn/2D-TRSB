using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Chassis chassis;
    public Rigidbody2D rb;
    public Transform groundCheck;
    public Collider2D collider;
    public bool grounded;

    public float health;
    public float charge;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        Debug.Log(grounded);
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

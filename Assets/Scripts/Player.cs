using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;

    public bool onGround = true;

    PlayerInput playerInput;
    Animator animator;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = playerInput.actions["Move"].ReadValue<Vector2>();

        rb.linearVelocityX = move.x * speed;
        animator.SetBool("isRunning", Mathf.Abs(move.x) > 0.1f);

        if (move.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z);
        }
        else if(move.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z);
        }

        if (onGround)
        {
            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                rb.linearVelocityY = jumpSpeed;
            }
        }

        animator.SetBool("isJumping", Mathf.Abs(rb.linearVelocityY) > 0.1f);
    }

    
    private void OnCollisionStay2D(Collision2D collision)
    {
        onGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }
}

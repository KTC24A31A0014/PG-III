using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;

    private bool onGround = false;

    PlayerInput playerInput;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = playerInput.actions["Move"].ReadValue<Vector2>();

        rb.linearVelocityX = move.x * speed;

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
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        onGround = true;
        if (onGround)
        {
            if (playerInput.actions["Jump"].WasPressedThisFrame())
            {
                rb.linearVelocityY = jumpSpeed;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }
}

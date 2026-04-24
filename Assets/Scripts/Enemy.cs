using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;

    Rigidbody2D rb;
    Vector2 direction = Vector2.left;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (direction.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z);
        }
        else if (direction.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z);
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

            other.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, 10f);
        }
    }
}
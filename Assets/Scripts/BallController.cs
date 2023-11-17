using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] protected float speed = 6f;
    protected Rigidbody2D rb;
    protected Vector2 direction;

    protected GameObject player;
    protected SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitCircle.normalized;

        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        rb.velocity = direction * speed;
        UpdateSpriteDirection();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        else
        {
            HandleCollision(collision);
        }
    }

    protected virtual void HandleCollision(Collision2D collision)
    {
        // Common collision handling for all balls
    }

    protected void UpdateSpriteDirection()
    {
        if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }
}

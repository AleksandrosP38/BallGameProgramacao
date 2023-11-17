using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedBallController : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    private Rigidbody2D rb;
    private Vector2 direction;

    private GameObject player;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitCircle.normalized;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveRedBall();
        UpdateSpriteDirection();
    }

    void MoveRedBall()
    {
        rb.velocity = direction * speed;
    }

    void UpdateSpriteDirection()
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

    private void OnCollisionEnter2D(Collision2D collision)
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

    private void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BolaForteVermelha") || collision.gameObject.CompareTag("BolaVerde"))
        {
            Vector2 playerPosition = player.transform.position;
            direction = (playerPosition - (Vector2)transform.position).normalized;
        }
        else
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }
    }
}

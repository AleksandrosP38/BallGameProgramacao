using System.Collections;
using UnityEngine;

public class BolaForteVermelha : BallController
{
    protected override void HandleCollision(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls") || collision.gameObject.CompareTag("BolaVerde") || collision.gameObject.CompareTag("BolaVermelha"))
        {
            Vector2 playerPosition = player.transform.position;
            direction = (playerPosition - (Vector2)transform.position).normalized;
        }
        else if (collision.gameObject.CompareTag("BolaForteVermelha") || collision.gameObject.CompareTag("BolaVerde"))
        {
            // Bounce off like walls
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }
    }
}

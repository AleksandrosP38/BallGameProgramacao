using UnityEngine;

public class BolaVerde : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private GameObject RedSpawn;
    [SerializeField] private GameObject RedSpawn2;
    private float RedSpawnRangeX = 10f;
    private float RedSpawnRangeY = 4f;

    private Vector2 direction;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Random.insideUnitCircle.normalized;
    }

    void Update()
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HandlePlayerCollision();
        }
        else if (collision.gameObject.CompareTag("Walls") || collision.gameObject.CompareTag("BolaVermelha") || collision.gameObject.CompareTag("BolaForteVermelha"))
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);
        }
    }

    private void HandlePlayerCollision()
    {
        Vector2 newPosition;

        do
        {
            newPosition = new Vector2(Random.Range(-7f, 7f), Random.Range(-3f, 3f));
        } while (!IsInCameraBounds(newPosition));

        transform.position = newPosition;

        Camera.main.backgroundColor = Random.ColorHSV();

        ScoreManager.Instance.IncreasePoints();
        SpawnObject();
    }

    private bool IsInCameraBounds(Vector2 position)
    {
        Camera mainCamera = Camera.main;

        float halfWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float halfHeight = mainCamera.orthographicSize;

        return (position.x > -halfWidth && position.x < halfWidth &&
                position.y > -halfHeight && position.y < halfHeight);
    }


    private void SpawnObject()
    {
        int randomSpawn = Random.Range(0, 2);

        float randomX, randomY;
        Vector2 spawnPosition;

        do
        {
            randomX = Random.Range(-RedSpawnRangeX, RedSpawnRangeX);
            randomY = Random.Range(-RedSpawnRangeY, RedSpawnRangeY);
            spawnPosition = new Vector2(randomX, randomY);
        } while (Vector2.Distance(spawnPosition, transform.position) < 2f);

        GameObject objectToSpawn = (randomSpawn == 0) ? RedSpawn : RedSpawn2;
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }
}

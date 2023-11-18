using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController: MonoBehaviour
{
    [SerializeField] private float powerUpDuration = 10f;
    [SerializeField] private float scalingFactor = 0.5f;
    [SerializeField] private float respawnDelay = 5f;
    [SerializeField] private float minDistanceToWall = 1.0f;

    private bool isActivated = false;
    private SpriteRenderer powerUpSpriteRenderer;
    private BoxCollider2D powerUpCollider;

    private Dictionary<GameObject, Vector3> originalScales = new Dictionary<GameObject, Vector3>();

    private Vector3 initialPosition;

    private void Start()
    {
        powerUpSpriteRenderer = GetComponent<SpriteRenderer>();
        powerUpCollider = GetComponent<BoxCollider2D>();

        powerUpSpriteRenderer.enabled = false;
        powerUpCollider.enabled = false;

        initialPosition = transform.position;

        StartCoroutine(StartPowerUp());
    }

    IEnumerator StartPowerUp()
    {
        yield return new WaitForSeconds(10f);

        powerUpSpriteRenderer.enabled = true;
        powerUpCollider.enabled = true;

        RespawnPowerUp();
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        RespawnPowerUp();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            ActivatePowerUp();
        }
    }

    void ActivatePowerUp()
    {
        ScaleDownGameObjectsWithTag("BolaForteVermelha");
        ScaleDownGameObjectsWithTag("BolaVermelha");

        powerUpSpriteRenderer.enabled = false;
        powerUpCollider.enabled = false;

        StopAllCoroutines();
        StartCoroutine(ReactivatePowerUp());
    }

    IEnumerator ReactivatePowerUp()
    {
        yield return new WaitForSeconds(powerUpDuration);

        ScaleUpGameObjectsWithTag("BolaForteVermelha");
        ScaleUpGameObjectsWithTag("BolaVermelha");

        powerUpSpriteRenderer.enabled = true;
        powerUpCollider.enabled = true;

        isActivated = false;

        StartCoroutine(SpawnDelay());
    }

    void RespawnPowerUp()
    {
        Vector3 randomPosition;

        int maxAttempts = 10; 

        for (int i = 0; i < maxAttempts; i++)
        {
            randomPosition = new Vector3(
                Random.Range(initialPosition.x - 5f, initialPosition.x + 5f),
                Random.Range(initialPosition.y - 5f, initialPosition.y + 5f),
                0f
            );

            if (!IsTooCloseToWall(randomPosition))
            {
                transform.position = randomPosition;

                powerUpSpriteRenderer.enabled = true;
                powerUpCollider.enabled = true;

                StartCoroutine(SpawnDelay());
                return;
            }
        }

        Debug.LogWarning("Couldn't find a suitable respawn position after multiple attempts.");
    }

    bool IsTooCloseToWall(Vector3 position)
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Walls");

        foreach (GameObject wall in walls)
        {
            if (Vector3.Distance(position, wall.transform.position) < minDistanceToWall)
            {
                return true; 
            }
        }

        return false; 
    }

    void ScaleDownGameObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            originalScales[obj] = obj.transform.localScale;
            obj.transform.localScale *= scalingFactor;
        }
    }

    void ScaleUpGameObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            if (originalScales.TryGetValue(obj, out Vector3 originalScale))
            {
                obj.transform.localScale = originalScale;
                originalScales.Remove(obj);
            }
        }
    }
}

using UnityEngine;

public class SpriteSpawner : MonoBehaviour
{
    public GameObject fallingSpritePrefab;  // Drag your sprite prefab here
    public float spawnInterval = 2f;        // Time between spawns
    public float spawnXMin = -5f;           // Left boundary for random X
    public float spawnXMax = 5f;            // Right boundary for random X

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnSprite();
            timer = 0f;
        }
    }

    void SpawnSprite()
    {
        // Pick a random X position within bounds
        float randomX = Random.Range(spawnXMin, spawnXMax);

        Vector3 spawnPos = new Vector3(randomX, transform.position.y, 0f);
        Instantiate(fallingSpritePrefab, spawnPos, Quaternion.identity);
    }
}

using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Array to hold different enemy prefabs
    public float spawnInterval = 3f;  // Time interval between spawns
    public float spawnDistance = 1f;  // How close to the player to spawn enemies outside the camera

    private Camera mainCamera;  // Reference to the main camera
    public Transform player;  // Reference to the player’s transform

    void Start()
    {
        mainCamera = Camera.main;  // Get the main camera
        InvokeRepeating("SpawnEnemy", 0f, spawnInterval);  // Call the SpawnEnemy method repeatedly
    }

    void SpawnEnemy()
    {
        if (player == null)
            return;
        // Get the screen bounds of the camera in world space
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Get player's position
        Vector2 playerPosition = player.position;

        // Randomly pick a side outside of the camera's view, as close as possible to the player
        int spawnSide = Random.Range(0, 4);  // 0 = top, 1 = bottom, 2 = left, 3 = right

        Vector2 spawnPosition = new Vector2();

        switch (spawnSide)
        {
            case 0:  // Top
                spawnPosition = new Vector2(
                    Random.Range(playerPosition.x - spawnDistance, playerPosition.x + spawnDistance),
                    screenBounds.y + spawnDistance
                );
                break;
            case 1:  // Bottom
                spawnPosition = new Vector2(
                    Random.Range(playerPosition.x - spawnDistance, playerPosition.x + spawnDistance),
                    -screenBounds.y - spawnDistance
                );
                break;
            case 2:  // Left
                spawnPosition = new Vector2(
                    -screenBounds.x - spawnDistance,
                    Random.Range(playerPosition.y - spawnDistance, playerPosition.y + spawnDistance)
                );
                break;
            case 3:  // Right
                spawnPosition = new Vector2(
                    screenBounds.x + spawnDistance,
                    Random.Range(playerPosition.y - spawnDistance, playerPosition.y + spawnDistance)
                );
                break;
        }

        // Randomly select one of the enemy types
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);  // Select a random enemy type

        // Instantiate the selected enemy at the chosen position
        Instantiate(enemyPrefabs[randomEnemyIndex], spawnPosition, Quaternion.identity);
    }
}
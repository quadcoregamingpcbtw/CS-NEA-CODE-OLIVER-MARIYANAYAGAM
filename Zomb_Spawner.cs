using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zomb_Spawner : MonoBehaviour
{
    public Transform ZombieSpawner; // Reference to the zombie spawn point
    public int wave; // Current wave number
    private bool waveFinished = false; // Tracks if the current wave is completed
    private float spawnRadius = 3f; // Radius within which zombies will spawn
    public TextMeshProUGUI waveDisplay; // UI display for wave count
    public GameObject boss; // Reference to the boss enemy prefab

    // Start is called before the first frame update
    void Start()
    {
        wave = 0; // Initialize wave count to zero
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the current wave is ongoing
        if (!waveFinished)
        {
            // If no zombies are left, mark the wave as finished
            if (Zombie_Handler.Instance.numOfZombies <= 0)
            {
                waveFinished = true;
            }
        }
        else
        {
            // Increase the wave count
            wave += 1;

            // Check if the player has reached wave 6 (victory condition)
            if (wave == 6)
            {
                print("You won");
            }

            // Save the highest wave reached if it's greater than the previous high score
            if (wave > SaveManager.Instance.LoadHighScore())
            {
                SaveManager.Instance.SaveHighScore(wave);
            }

            // Check if it's the boss wave
            if (wave == 5)
            {
                // Generate a random spawn location for the boss
                float x = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
                float z = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
                Vector3 spawnLocation = ZombieSpawner.position + new Vector3(x, 0, z);

                // Activate the boss fight
                Zombie_Handler.Instance.BossIsActive = true;
                GameObject bossSpawn = Instantiate(boss, spawnLocation, Quaternion.identity);
                Zombie_Handler.Instance.numOfZombies = 100000; // Set a high number to prevent wave completion

                // Spawn additional zombies alongside the boss
                for (int i = 0; i < 20; i++)
                {
                    float x1 = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
                    float z1 = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
                    Vector3 spawnLocation1 = ZombieSpawner.position + new Vector3(x1, 0, z1);

                    GameObject zomb = Instantiate(Zombie_Handler.Instance.ZombiePrefab, spawnLocation1, Quaternion.identity);
                    print("Spawned zombie at: " + spawnLocation1);
                }

                // Update wave UI display
                waveDisplay.text = $"Wave: {wave}/5";
                waveFinished = false; // Reset wave completion status
            }
            else
            {
                // Normal wave spawn logic
                Zombie_Handler.Instance.numOfZombies = wave * 5; // Increase zombies per wave
                waveDisplay.text = $"Wave: {wave}/5"; // Update wave UI

                // Spawn the determined number of zombies for this wave
                for (int i = 0; i < Zombie_Handler.Instance.numOfZombies; i++)
                {
                    float x = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
                    float z = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
                    Vector3 spawnLocation = ZombieSpawner.position + new Vector3(x, 0, z);

                    GameObject zomb = Instantiate(Zombie_Handler.Instance.ZombiePrefab, spawnLocation, Quaternion.identity);
                    print("Spawned zombie at: " + spawnLocation);
                }
                waveFinished = false; // Reset wave completion status
            }
        }
    }
}

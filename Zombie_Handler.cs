using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Zombie_Handler : MonoBehaviour
{
    // Singleton instance of Zombie_Handler
    public static Zombie_Handler Instance { get; set; }
    // Prefab for spawning zombies
    public GameObject ZombiePrefab;
    // Number of zombies to spawn
    public int numOfZombies = 1;
    // Indicates if the boss is currently active
    public bool BossIsActive = true;
    // Indicates if the current round is a danger round
    public bool dangerRound = false;
    // Level of danger in the current round
    public float dangerLevel = 10f;
    // UI text to display boss health
    public TextMeshProUGUI bossHealthDisplay;
    // UI screen displayed when the player wins
    public GameObject winningScreen;

    private void Awake()
    {
        // Ensure only one instance of Zombie_Handler exists (Singleton pattern)
        if (Instance != null && Instance != this)
        {
            // Destroy any duplicate instances
            Destroy(gameObject);
        }
        else
        {
            // Set the instance to this object
            Instance = this;
        }
    }
}

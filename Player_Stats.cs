using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player_Stats : MonoBehaviour
{
    // Singleton instance of Player_Stats
    public static Player_Stats Instance { get; set; }

    // Player attributes
    public float Health = 100f; // Player health
    public float shootTime = 0.3f; // Time between shots
    public float damage = 30f; // Damage per shot
    public float spread = 0.1f; // Bullet spread
    public float ammo = 100f; // Maximum ammo capacity
    public float currentAmmo = 100f; // Current ammo count
    public float mode = 1; // Fire mode (0 = auto, 1 = single)
    public bool dead = false; // Player death state

    // Currency system
    public float money = 0f; // Player's money amount
    public TextMeshProUGUI coinsDisplay; // UI display for coins
    public TextMeshProUGUI healthDisplay; // UI display for health
    public TextMeshProUGUI ammoDisplay; // UI display for ammo

    public Transform Player; // Player transform reference
    public GameObject deathScreen; // UI death screen

    private void Awake()
    {
        // Ensure only one instance of Player_Stats exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
        else
        {
            Instance = this; // Set the instance to this object
        }
    }

    private void Update()
    {
        // Check if player's health has reached zero
        if (Health <= 0)
        {
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        dead = true; // Mark player as dead

        // Disable player movement and mouse controls
        GetComponent<Mouse_Mov>().enabled = false;
        GetComponent<Player_Mov>().enabled = false;

        // Unlock and show the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // Show the death screen UI
        deathScreen.SetActive(true);
    }
}

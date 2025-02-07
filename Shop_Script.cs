using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop_Script : MonoBehaviour
{
    // Singleton instance of Shop_Script
    public static Shop_Script Instance { get; set; }
    // Currently equipped gun
    public string gunEquipped = "Pistol";
    // Reference to the AK47 GameObject
    public GameObject AK47;
    // UI text to display price
    public TextMeshProUGUI priceDisplay;
    // Dictionary storing stats for various guns and items
    public Dictionary<string, float> GunStats = new Dictionary<string, float>()
    {
        // AK47 stats
        {"AK47shootTime", 0.2f},
        {"AK47damage", 40f},
        {"AK47spread", 0.1f},
        {"AK47ammo", 100f},
        {"AK47mode", 0f}, // 0 = auto, 1 = single
        {"AK47price", 100f},

        // Uzi stats
        {"UzishootTime", 0.05f},
        {"Uzidamage", 20f},
        {"Uzispread", 0.6f},
        {"Uziammo", 300f},
        {"Uzimode", 0f}, // 0 = auto, 1 = single
        {"Uziprice", 400f},

        // RPG stats
        {"RPGshootTime", 1.5f},
        {"RPGdamage", 500f},
        {"RPGspread", 0.01f},
        {"RPGammo", 10f},
        {"RPGmode", 1f}, // 0 = auto, 1 = single
        {"RPGprice", 1000f},

        // Medkit price
        {"Medkitprice", 100f}
    };

    private void Awake()
    {
        // Ensure only one instance of Shop_Script exists (Singleton pattern)
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

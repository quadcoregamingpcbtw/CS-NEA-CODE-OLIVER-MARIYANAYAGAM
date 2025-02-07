using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Stats : MonoBehaviour
{
    // Singleton instance of Zombie_Stats
    public static Zombie_Stats Instance { get; set; }

    // Float for health
    public float Health = 100f;

    private void Awake()
    {
        // Ensure only one instance of Zombie_Stats exists
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    // Singleton instance of SoundHandler
    public static SoundHandler Instance { get; set; }

    // Reference to Pistol shooting sound
    public AudioSource PistolShootSound;

    private void Awake()
    {
        // Ensure only one instance of SoundHandler exists
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

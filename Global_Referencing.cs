using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Referencing : MonoBehaviour
{
    // Singleton instance of GlobalReferences
    public static Global_Referencing Instance { get; set; }

    // Prefab for the bullet impact effect
    public GameObject bulletImpactPrefab;

    private void Awake()
    {
        // Ensure only one instance of GlobalReferences exists
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
